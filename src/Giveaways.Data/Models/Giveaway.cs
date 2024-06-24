using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaways.Data;

/// <summary>
/// Represents a giveaway hosted by a user.
/// </summary>
public class Giveaway
{
    /// <summary>
    /// Gets the message identifier of the giveaway message.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required ulong MessageId { get; init; }

    /// <summary>
    /// Gets the channel identifier where this giveaway was hosted.
    /// </summary>
    public required ulong ChannelId { get; init; }

    /// <summary>
    /// Gets the guild identifier where this giveaway was hosted.
    /// </summary>
    public required ulong GuildId { get; init; }

    /// <summary>
    /// Gets or sets the prize of this giveaway.
    /// </summary>
    public required string Prize { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of winners for this giveaway.
    /// </summary>
    public required int MaxWinners { get; set; }

    /// <summary>
    ///  Gets or sets the expiration date and time for this giveaway.
    /// </summary>
    public required DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the current status of this giveaway.
    /// </summary>
    public GiveawayStatus Status { get; set; } = GiveawayStatus.Active;

    /// <summary>
    /// Gets the list of participants for this giveaway.
    /// </summary>
    public List<GiveawayParticipant> Participants { get; } = [];
}
