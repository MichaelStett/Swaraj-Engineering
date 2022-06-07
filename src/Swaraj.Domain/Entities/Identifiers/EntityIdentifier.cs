using System;

using Newtonsoft.Json;

namespace Swaraj.Domain.Entities.Identifiers
{
    public class EntityIdentifier
        : IComparable<EntityIdentifier>, IEquatable<EntityIdentifier>
    {
        public Guid Value { get; }

        public EntityIdentifier(Guid value)
        {
            Value = value;
        }

        public bool Equals(EntityIdentifier other) => this.Value.Equals(other.Value);
        public int CompareTo(EntityIdentifier other) => Value.CompareTo(other.Value);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is EntityIdentifier other && Equals(other);
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();

        public static bool operator ==(EntityIdentifier a, EntityIdentifier b) => a.CompareTo(b) == 0;
        public static bool operator !=(EntityIdentifier a, EntityIdentifier b) => !(a == b);
    }
}
