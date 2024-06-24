using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Giveaways.Data;

/// <summary>
/// Represents a participant of a giveaway.
/// </summary>
[PrimaryKey(nameof(UserId), nameof(GiveawayId))]
public class GiveawayParticipant
{
    /// <summary>
    /// Gets or sets the user ID of the participant.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public ulong UserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the participant is a winner.
    /// </summary>
    public bool IsWinner { get; set; } = false;

    /// <summary>
    /// Gets or sets the ID of the giveaway the participant is associated with.
    /// </summary>
    public ulong GiveawayId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the giveaway the participant is associated with.
    /// </summary>
    public Giveaway Giveaway { get; set; } = null!;
}
