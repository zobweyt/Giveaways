using System;
using Discord;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Giveaways.Data;

/// <summary>
/// Represents an abstract class for entities with a key identifier.
/// </summary>
/// <typeparam name="TId">The type of the key identifier.</typeparam>
public abstract class KeyedEntity<TId> : IEntity<TId>
    where TId : IEquatable<TId>
{
    public TId Id { get; init; } = default!;
}

/// <summary>
/// Represents an abstract class for configuring entities with a key identifier.
/// </summary>
/// <typeparam name="TId">The type of the key identifier.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
internal abstract class KeyedEntityConfiguration<TId, TEntity> : IEntityTypeConfiguration<TEntity>
    where TId : IEquatable<TId>
    where TEntity : KeyedEntity<TId>
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}
