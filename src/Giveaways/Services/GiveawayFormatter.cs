using System.Linq;
using Discord;
using Discord.WebSocket;
using Giveaways.Data;

namespace Giveaways.Services;

public class GiveawayFormatter
{
    private readonly DiscordSocketClient _client;

    public GiveawayFormatter(DiscordSocketClient client)
    {
        _client = client;
    }

    public MessageProperties GetActiveMessageProperties(Giveaway giveaway)
    {
        var relative = TimestampTag.FormatFromDateTime(giveaway.ExpiresAt, TimestampTagStyles.Relative);
        var longDateTime = TimestampTag.FormatFromDateTime(giveaway.ExpiresAt, TimestampTagStyles.LongDateTime);

        var embed = new EmbedBuilder()
            .WithAuthor($"x{giveaway.MaxWinners} Giveaway Prizes", Icons.Gift)
            .WithTitle(giveaway.Prize)
            .AddField("Winners Selection Date", $"{relative} ({longDateTime})")
            .WithFooter($"{giveaway.Participants.Count} participants")
            .WithColor(Colors.Fuchsia)
            .Build();

        var components = new ComponentBuilder()
            .WithButton("Join", $"join:{giveaway.MessageId}", ButtonStyle.Primary, new Emoji("🪅"))
            .WithButton("Learn more", $"info:{giveaway.MessageId}", ButtonStyle.Secondary, new Emoji("❓"))
            .Build();

        return new MessageProperties
        {
            Embed = embed,
            Components = components
        };
    }

    public MessageProperties GetEndedMessageProperties(Giveaway giveaway)
    {
        var ids = giveaway.Winners.Select(w => MentionUtils.MentionUser(w.UserId));
        var mentions = string.Join(", ", ids);

        var embed = new EmbedBuilder()
            .WithAuthor($"x{giveaway.MaxWinners} Giveaway Prizes", Icons.Confetti)
            .WithTitle(giveaway.Prize)
            .AddField("Winners", string.IsNullOrEmpty(mentions) ? "None" : mentions)
            .WithFooter($"{giveaway.Participants.Count} participants")
            .WithColor(Colors.Secondary)
            .WithCurrentTimestamp()
            .Build();

        return new MessageProperties()
        {
            Embed = embed,
            Components = null
        };
    }

    public MessageProperties GetInfoMessageProperties(Giveaway giveaway)
    {
        var guild = _client.GetGuild(giveaway.GuildId);

        var longDate = TimestampTag.FormatFromDateTime(giveaway.ExpiresAt, TimestampTagStyles.LongDate);
        var longDateTime = TimestampTag.FormatFromDateTime(giveaway.ExpiresAt, TimestampTagStyles.LongDateTime);

        var embed = new EmbedBuilder()
            .WithTitle("About this event")
            .AddField("How it works?", $"On {longDate} the app is going to choose **{giveaway.MaxWinners}** random winners.")
            .AddField("Who can join?", $"{guild.EveryoneRole.Mention} can push the join button before {longDateTime} in order to enter.")
            .WithFooter($"The event organizers of {guild.Name} are responsible for awarding prizes.")
            .WithColor(Colors.Primary)
            .Build();

        return new MessageProperties
        {
            Embed = embed
        };
    }

    public MessageProperties GetCongratsMessageProperties(GiveawayParticipant winner)
    {
        var guild = _client.GetGuild(winner.Giveaway.GuildId);

        var embed = new EmbedBuilder()
            .WithAuthor($"Congrats!", Icons.Confetti)
            .WithTitle($"You won the {winner.Giveaway.Prize}!")
            .WithDescription($"To get your prize, visit **{guild.Name}** and contact their event organizers.")
            .WithFooter("The app is not responsible for awarding prizes.")
            .WithColor(Colors.Fuchsia)
            .Build();

        var url = MessageUtils.FormatJumpUrl(winner.Giveaway.GuildId, winner.Giveaway.ChannelId, winner.Giveaway.MessageId);

        var components = new ComponentBuilder()
            .WithLink("Jump to Message", url, new Emoji("🎁"))
            .Build();

        return new MessageProperties
        {
            Embed = embed,
            Components = components
        };
    }
}
