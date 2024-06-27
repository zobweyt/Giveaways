using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Giveaways.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Giveaways.Services;

public class GiveawayService
{
    private readonly DiscordSocketClient _client;
    private readonly ILogger<GiveawayService> _logger;
    private readonly AppDbContext _db;
    private readonly GiveawayFormatter _formatter;

    public GiveawayService(DiscordSocketClient client, ILogger<GiveawayService> logger, AppDbContext db, GiveawayFormatter formatter)
    {
        _client = client;
        _logger = logger;
        _db = db;
        _formatter = formatter;
    }

    /// <summary>
    /// Adds or removes a participant from the <paramref name="giveaway">.
    /// </summary>
    /// <param name="giveaway">The giveaway object to add or remove the participant from.</param>
    /// <param name="userId">The ID of the user to add or remove as a participant.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddOrRemoveParticipantAsync(Giveaway giveaway, ulong userId)
    {
        var participant = giveaway.Participants.FirstOrDefault(p => p.UserId == userId);

        if (participant == null)
            giveaway.Participants.Add(new() { UserId = userId });
        else
            _db.Remove(participant);

        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Immediately ends the giveaway associated with the <paramref name="messageId"/>.
    /// </summary>
    /// <param name="messageId">The message ID associated with a giveaway.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [DisableConcurrentExecution(60)]
    public async Task ExpireAsync(ulong messageId)
    {
        var giveaway = await _db.Giveaways
            .Include(g => g.Participants)
            .FirstOrDefaultAsync(g => g.MessageId == messageId && g.Status != GiveawayStatus.Ended);

        if (giveaway == null)
            return;

        await ExpireAsync(giveaway);
    }

    /// <summary>
    /// Immediately ends the <paramref name="giveaway"/>.
    /// </summary>
    /// <param name="giveaway">The giveaway to end immediately.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [DisableConcurrentExecution(60)]
    public async Task ExpireAsync(Giveaway giveaway)
    {
        if (await GetGiveawayMessage(giveaway) is not IUserMessage message)
            return;

        await SummarizeAsync(giveaway, message);
    }

    /// <summary>
    /// Gets the message associated with the <paramref name="giveaway"/>.
    /// </summary>
    /// <param name="giveaway">The giveaway for which to retrieve the associated message.</param>
    /// <returns>The message associated with the giveaway, or null if not found.</returns>
    public async Task<IMessage?> GetGiveawayMessage(Giveaway giveaway)
    {
        if (_client.GetChannel(giveaway.ChannelId) is ITextChannel channel)
            return await channel.GetMessageAsync(giveaway.MessageId);
        return null;
    }

    private async Task SummarizeAsync(Giveaway giveaway, IUserMessage message)
    {
        giveaway.Status = GiveawayStatus.Ended;
        await SetRandomWinners(giveaway);

        var modifiedProps = _formatter.GetEndedMessageProperties(giveaway);
        await message.ModifyAsync(props =>
        {
            props.Embed = modifiedProps.Embed;
            props.Components = modifiedProps.Components;
        });

        await _db.SaveChangesAsync();
    }

    private async Task SetRandomWinners(Giveaway giveaway)
    {
        var participants = giveaway.Participants.Shuffle().Take(giveaway.MaxWinners);

        foreach (var participant in participants)
        {
            await CongratulateWinner(participant);

            participant.IsWinner = true;
        }
    }

    private async Task CongratulateWinner(GiveawayParticipant winner)
    {
        var user = await _client.GetUserAsync(winner.UserId);
        var props = _formatter.GetCongratsMessageProperties(winner);

        try
        {
            await user.SendMessageAsync(embed: props.Embed.Value, components: props.Components.Value);
        }
        catch (HttpException)
        {
            _logger.LogDebug("Cannot send message to {username}.", user.Username);
        }
    }
}
