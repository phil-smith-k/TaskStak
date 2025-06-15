namespace TaskStak.CLI.Models
{
    /// <summary>
    /// Provides a type-safe wrapper for working with bitwise flags of an enumeration type.
    /// </summary>
    /// <remarks>This class allows manipulation of bitwise flags for a specified enumeration type, including
    /// adding, removing, and checking for specific flags. It is designed to simplify working with enums that are
    /// marked with the <see cref="FlagsAttribute"/>.</remarks>
    /// <typeparam name="TFlag">The enumeration type that represents the flags. Must be a value type and derive from <see cref="Enum"/>.</typeparam>
    public class Flags<TFlag> where TFlag : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Flags{TFlag}"/> class with the specified flag value.
        /// </summary>
        /// <param name="value">The initial value of the flag.</param>
        private Flags(TFlag value)
        {
            this.Value = value;
        }

        /// <summary>
        /// The current value of the flags.
        /// </summary>
        public TFlag Value { get; private set; }

        /// <summary>
        /// Determines whether ANY of the specified flags are enabled in the current flag value.
        /// </summary>
        /// <param name="flag">The flag to check against the current value.</param>
        /// <returns><see langword="true"/> if any bits in <paramref name="flag"/> are set in the current value; otherwise, <see
        /// langword="false"/>.</returns>
        public bool AnyOn(TFlag flag)
        {
            var current = (int)(object)this.Value;
            var check = (int)(object)flag;

            return (current & check) != 0;
        }

        /// <summary>
        /// Determines whether the current flag value is equal to the specified flag.
        /// </summary>
        /// <param name="flag">The flag to compare with the current value.</param>
        /// <returns><see langword="true"/> if the current value is equal to the specified flag; otherwise, <see
        /// langword="false"/>.</returns>
        public bool Is(TFlag flag)
        {
            var current = (int)(object)this.Value;
            var check = (int)(object)flag;

            return current == check;
        }

        /// <summary>
        /// Determines whether ALL the specified flags are enabled in the current flag value.
        /// </summary>
        /// <param name="flag">The flag to check for in the current value.</param>
        /// <returns><see langword="true"/> if the specified flag is set; otherwise, <see langword="false"/>.</returns>
        public bool IsOn(TFlag flag) 
            => this.Value.HasFlag(flag);

        /// <summary>
        /// Converts the specified <see cref="Flags{TFlag}"/> instance to an integer representation.
        /// </summary>
        /// <param name="flags">The <see cref="Flags{TFlag}"/> instance to convert.</param>
        public static implicit operator int(Flags<TFlag> flags)
        {
            return Convert.ToInt32(flags.Value);
        }

        /// <summary>
        /// Converts an integer value to a <see cref="Flags{TFlag}"/> instance.
        /// </summary>
        /// <param name="value">The integer value to convert to the corresponding flag enumeration.</param>
        public static explicit operator Flags<TFlag>(int value)
        {
            var enumValue = (TFlag)Enum.ToObject(typeof(TFlag), value);

            return From(enumValue);
        }

        /// <summary>
        /// Disables the specified flag in the current flag value.
        /// </summary>
        /// <remarks>This method removes the specified flag from the current value by performing a bitwise
        /// AND operation with the complement of the flag. The resulting value will no longer include the specified
        /// flag.</remarks>
        /// <param name="flag">The flag to be cleared. Must be a valid flag of the same type as the current value.</param>
        public void SetOff(TFlag flag)
        {
            var val = (int)(object)this.Value;
            val &= ~(int)(object)flag;

            this.Value = (TFlag)(object)val;
        }

        /// <summary>
        /// Enables the specified flag by performing a bitwise OR operation on the current flag value.
        /// </summary>
        /// <remarks>This method modifies the current value by enabling the bits corresponding to the
        /// specified flag. Ensure that <typeparamref name="TFlag"/> is an enumeration type that supports bitwise
        /// operations.</remarks>
        /// <param name="flag">The flag to set. Must be a valid value of the <typeparamref name="TFlag"/> enumeration.</param>
        public void SetOn(TFlag flag)
        {
            var val = (int)(object)this.Value;
            val |= (int)(object)flag;

            this.Value = (TFlag)(object)val;
        }

        /// <summary>
        /// Sets the current value of the flag to the specified value.
        /// </summary>
        /// <param name="flag">The new value to assign to the flag.</param>
        public void SetTo(TFlag flag) 
            => this.Value = flag;
        
        /// <summary>
        /// Creates a new instance of the <see cref="Flags{TFlag}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the flags. Defaults to the default value of <typeparamref name="TFlag"/> if not
        /// specified.</param>
        /// <returns>A new <see cref="Flags{TFlag}"/> instance initialized with the specified value.</returns>
        public static Flags<TFlag> From(TFlag value = default)
        {
            return new Flags<TFlag>(value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Flags{TFlag}"/> class with the specified integer value.
        /// </summary>
        /// <param name="value">The integer value to initialize the flags with. This value is cast to the underlying type of <typeparamref
        /// name="TFlag"/>.</param>
        /// <returns>A new <see cref="Flags{TFlag}"/> instance initialized with the specified value.</returns>
        public static Flags<TFlag> From(int value)
        {
            return new Flags<TFlag>((TFlag)(object)value);
        }
    }
}
