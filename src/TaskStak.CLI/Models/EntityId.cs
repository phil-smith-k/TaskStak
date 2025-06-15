namespace TaskStak.CLI.Models
{
    /// <summary>
    /// Represents a unique identifier for an entity.
    /// </summary>
    /// <remarks>The <see cref="EntityId"/> struct provides a lightweight, immutable representation of an
    /// entity's identifier. It is backed by a string value and supports equality comparison, implicit conversion to
    /// string, and custom formatting. Use <see cref="New"/> to generate a new unique identifier.</remarks>
    public readonly struct EntityId : IEquatable<EntityId>
    {
        private readonly string _value;

        private EntityId(string value) 
            => _value = value.ToUpperInvariant();

        public static EntityId New() 
            => new(Guid.NewGuid().ToString("N")[..8]);

        public static EntityId Parse(string value)
        {
            if (TryParse(value, out var result))
                return result;

            throw new FormatException($"'{value}' is not in a valid format for EntityId.");
        }

        public override string ToString() 
            => _value;

        public static implicit operator string(EntityId value) 
            => value._value;

        public bool Equals(EntityId other)
            => _value == other._value;

        public override bool Equals(object? obj)
            => obj is EntityId other && this.Equals(other);

        public override int GetHashCode()
            => _value.GetHashCode();

        public static bool operator ==(EntityId a, EntityId b) 
            => a.Equals(b);

        public static bool operator !=(EntityId a, EntityId b) 
            => !a.Equals(b);

        public static bool TryParse(string value, out EntityId result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(value))
                return false;
            
            if (value.Length != 8)
                return false;

            if (!IsValidHex(value))
                return false;

            result = new EntityId(value);
            return true;
        }

        private static bool IsValidHex(string value)
            => value.All(Uri.IsHexDigit);
    }
}
