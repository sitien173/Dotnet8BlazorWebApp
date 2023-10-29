using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebApp.Shared;

public abstract class Entity<TId> : Entity, IVersion
{
    [Key] public TId Id { get; set; }

    [Timestamp]
    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    public override bool Equals(object? obj)
    {
        if (obj is Entity<TId> other)
            return GetType() == other.GetType() && EqualityComparer<TId>.Default.Equals(Id, other.Id) &&
                   RowVersion.SequenceEqual(other.RowVersion);

        return false;
    }

    public override int GetHashCode()
    {
        // Generate a hash code based on the entity's type and ID
        unchecked
        {
            var hash = GetType().GetHashCode();
            hash = (hash * 397) ^ EqualityComparer<TId>.Default.GetHashCode(Id);
            return hash;
        }
    }
}

public abstract class Entity
{
}