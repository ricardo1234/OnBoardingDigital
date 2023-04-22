namespace OnBoardingDigital.Domain.Common;

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);
    public bool Equals(Entity<TId>? other) => Equals(other);
    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);
    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);
    public override int GetHashCode() => Id.GetHashCode();

#pragma warning disable CS8618
    protected Entity()
    { }
#pragma warning restore CS8618
}