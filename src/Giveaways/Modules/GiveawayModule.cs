using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Giveaways.Data;
using Giveaways.Services;
using Hangfire.States;
using Microsoft.EntityFrameworkCore;

namespace Giveaways.Modules;

[RequireContext(ContextType.Guild)]
public class GiveawayModule : ModuleBase
{
    private readonly AppDbContext _db;
    private readonly GiveawayFormatter _formatter;
    private readonly GiveawayScheduler _scheduler;
    private readonly GiveawayService _service;

    public GiveawayModule(AppDbContext db, GiveawayFormatter formatter, GiveawayScheduler scheduler, GiveawayService service)
    {
        _db = db;
        _formatter = formatter;
        _scheduler = scheduler;
        _service = service;
    }

    [DefaultMemberPermissions(GuildPermission.ManageEvents)]
    [SlashCommand("start", "Starts a new giveaway in the current channel.")]
    public async Task Start([ComplexParameter] GiveawayStartArgs args)
    {
        await DeferAsync();
        var response = await GetOriginalResponseAsync();

        var giveaway = new Giveaway()
        {
            MessageId = response.Id,
            ChannelId = Context.Channel.Id,
            GuildId = Context.Guild.Id,
            Prize = args.Prize,
            MaxWinners = args.MaxWinners,
            ExpiresAt = args.ExpiresAt,
        };

        await _db.AddAsync(giveaway);
        await _db.SaveChangesAsync();

        _scheduler.Schedule(giveaway.MessageId, giveaway.ExpiresAt);

        var props = _formatter.GetActiveMessageProperties(giveaway);
        await FollowupAsync(embed: props.Embed.Value, components: props.Components.Value);
    }

    [ComponentInteraction("join:*")]
    public async Task<RuntimeResult> Join(ulong messageId)
    {
        await DeferAsync(true);

        var giveaway = await _db.Giveaways
            .Include(g => g.Participants)
            .FirstOrDefaultAsync(g => g.MessageId == messageId && g.Status == GiveawayStatus.Active);

        if (giveaway == null)
            return InteractionResult.FromError("There's no active giveaway associated with this message.");

        if (await _service.GetGiveawayMessage(giveaway) is not IUserMessage message)
            return InteractionResult.FromError("Unknown message!");

        await _service.AddOrRemoveParticipantAsync(giveaway, Context.User.Id);

        var modifiedProps = _formatter.GetActiveMessageProperties(giveaway);
        await message.ModifyAsync(props =>
        {
            props.Embed = modifiedProps.Embed;
            props.Components = modifiedProps.Components;
        });

        return InteractionResult.FromSuccess();
    }

    [ComponentInteraction("info:*")]
    public async Task<RuntimeResult> Info(ulong messageId)
    {
        await DeferAsync(true);

        var giveaway = await _db.Giveaways
            .Include(g => g.Participants)
            .FirstOrDefaultAsync(g => g.MessageId == messageId && g.Status != GiveawayStatus.Ended);

        if (giveaway == null)
            return InteractionResult.FromError("There's no active giveaway associated with this message.");

        var props = _formatter.GetInfoMessageProperties(giveaway);
        await FollowupAsync(embed: props.Embed.Value, ephemeral: true);

        return InteractionResult.FromSuccess();
    }

    [DefaultMemberPermissions(GuildPermission.ManageEvents)]
    [MessageCommand("End Giveaway")]
    public async Task<RuntimeResult> End(IMessage message)
    {
        await DeferAsync(true);

        if (await _db.Giveaways.AnyAsync(g => g.MessageId == message.Id && g.Status == GiveawayStatus.Ended))
            return InteractionResult.FromError("There's no active giveaway associated with this message.");

        await _service.ExpireAsync(message.Id);
        _scheduler.ChangeState(message.Id, new SucceededState(0, 0, 0));

        return InteractionResult.FromSuccess("This giveaway has just been ended! Winners have been notified via DMs.");
    }
}
