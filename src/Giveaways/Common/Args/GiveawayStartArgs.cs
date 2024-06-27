using System;
using Discord.Interactions;

namespace Giveaways;

[method: ComplexParameterCtor]
public class GiveawayStartArgs(
    [MinLength(2)]
    [MaxLength(128)]
    [Summary(description: "The prize of the giveaway.")]
    string prize,

    [MinValue(1)]
    [MaxValue(25)]
    [Summary(description: "The maximum number of winners for the giveaway.")]
    int winners,

    [Choice("1 hour", "1h")]
    [Choice("4 hours", "4h")]
    [Choice("8 hours", "8h")]
    [Choice("1 day", "1d")]
    [Choice("3 days", "3d")]
    [Choice("1 week", "7d")]
    [Summary(description: "The duration of the giveaway.")]
    TimeSpan duration)
{
    public string Prize => prize;
    public int MaxWinners => winners;
    public DateTime ExpiresAt => DateTime.Now + duration;
}
