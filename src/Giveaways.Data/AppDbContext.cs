using Microsoft.EntityFrameworkCore;

namespace Giveaways.Data;

/// <summary>
/// An implementation of the <see cref="DbContext"/> for this solution.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AppDbContext"/>.
/// </remarks>
/// <param name="options">The options for this context.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Giveaway> Giveaways { get; set; }
    public DbSet<GiveawayParticipant> GiveawayParticipants { get; set; }
}
