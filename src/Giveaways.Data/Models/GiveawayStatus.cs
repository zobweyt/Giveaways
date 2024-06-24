namespace Giveaways;

/// <summary>
/// Represents the status of a giveaway.
/// </summary>
public enum GiveawayStatus
{
    /// <summary>
    /// The giveaway is currently active and participants can still enter.
    /// </summary>
    Active,

    /// <summary>
    /// The giveaway is temporarily suspended and not accepting new entries.
    /// </summary>
    Suspended,

    /// <summary>
    /// The giveaway has ended, and winners have been selected.
    /// </summary>
    Ended,
}
