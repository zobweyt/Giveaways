namespace Giveaways;

/// <summary>
/// Provides a series of helper methods for messages.
/// </summary>
public static class MessageUtils
{
    /// <summary>
    /// Formats the jump URL for a specific message in a guild.
    /// </summary>
    /// <param name="guildId">The ID of the guild.</param>
    /// <param name="channelId">The ID of the channel where the message is located.</param>
    /// <param name="messageId">The ID of the message for which to generate the jump URL.</param>
    /// <returns>The jump URL that directs to the message.</returns>
    public static string FormatJumpUrl(ulong guildId, ulong channelId, ulong messageId)
        => $"https://discord.com/channels/{guildId}/{channelId}/{messageId}";
}
