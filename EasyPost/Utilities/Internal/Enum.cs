using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Internal
{
#pragma warning disable SA1649
    /// <summary>
    ///     Interface for all custom enums.
    /// </summary>
    public interface IEnum
#pragma warning restore SA1649
    {
    }

    /// <summary>
    ///     A Java-like enum implementation for C#.
    /// </summary>
#pragma warning disable CA1716
    public abstract class Enum : IComparable, IEnum
#pragma warning restore CA1716
    {
        /// <summary>
        ///     An internal ID associated with this <see cref="Enum"/>, used for comparison and equality.
        /// </summary>
        private int Id { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enum" /> class.
        /// </summary>
        /// <param name="id">The internal ID to associated with this enum.</param>
        protected Enum(int id) => Id = id;

        /// <inheritdoc cref="int.CompareTo(int)"/>
        public int CompareTo(object? obj) => Id.CompareTo(((Enum)obj!).Id);

        /// <inheritdoc cref="int.ToString()"/>
        public override string ToString() => Id.ToString(CultureInfo.InvariantCulture);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object? obj)
        {
            try
            {
                if (GetType() != obj!.GetType())
                {
                    // types are not the same
                    return false;
                }

                Enum objEnum = (Enum)obj;
                return objEnum == this;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception)
            {
                // casting likely failed
                return false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

#pragma warning disable CA1307
        /// <inheritdoc cref="object.GetHashCode()"/>
        public override int GetHashCode() => new Dictionary<string, int> { { GetType().ToString(), Id } }.GetHashCode();
#pragma warning restore CA1307

        /// <inheritdoc cref="Equals(object)"/>
        private bool Equals(Enum? other) => Id == other?.Id;

        /// <summary>
        ///     Compare two objects for equality.
        /// </summary>
        /// <param name="one">The first object in the comparison.</param>
        /// <param name="two">The second object in the comparison.</param>
        /// <returns><c>true</c> if the two objects are equal; otherwise, false.</returns>
        public static bool operator ==(Enum? one, Enum? two)
        {
            if (one is null && two is null)
            {
                return true;
            }

#pragma warning disable IDE0046
            if (one is null || two is null)
#pragma warning restore IDE0046
            {
                return false;
            }

            return one.Equals(two);
        }

        /// <summary>
        ///     Compare two objects for inequality.
        /// </summary>
        /// <param name="one">The first object in the comparison.</param>
        /// <param name="two">The second object in the comparison.</param>
        /// <returns><c>true</c> if the two objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Enum? one, Enum? two) => !(one == two);

        /// <summary>
        ///     Retrieve an <see cref="IEnumerable{T}"/> of all <see cref="Enum"/>s of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Enum"/> to retrieve all entries of.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="Enum"/> of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> GetAll<T>()
            where T : IEnum
            =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        /// <summary>
        ///     Compare two objects.
        /// </summary>
        /// <param name="left">The first object in the comparison.</param>
        /// <param name="right">The second object in the comparison.</param>
        /// <returns><c>true</c> if the left object is less than the right object, <c>false</c> otherwise.</returns>
        public static bool operator <(Enum left, Enum right) => left.CompareTo(right) < 0;

        /// <summary>
        ///     Compare two objects.
        /// </summary>
        /// <param name="left">The first object in the comparison.</param>
        /// <param name="right">The second object in the comparison.</param>
        /// <returns><c>true</c> if the left object is less than or equal to the right object, <c>false</c> otherwise.</returns>
        public static bool operator <=(Enum left, Enum right) => left.CompareTo(right) <= 0;

        /// <summary>
        ///     Compare two objects.
        /// </summary>
        /// <param name="left">The first object in the comparison.</param>
        /// <param name="right">The second object in the comparison.</param>
        /// <returns><c>true</c> if the left object is greater than the right object, <c>false</c> otherwise.</returns>
        public static bool operator >(Enum left, Enum right) => left.CompareTo(right) > 0;

        /// <summary>
        ///     Compare two objects.
        /// </summary>
        /// <param name="left">The first object in the comparison.</param>
        /// <param name="right">The second object in the comparison.</param>
        /// <returns><c>true</c> if the left object is greater than or equal to the right object, <c>false</c> otherwise.</returns>
        public static bool operator >=(Enum left, Enum right) => left.CompareTo(right) >= 0;
    }

    /// <summary>
    ///     An <see cref="Enum"/> that stores a value internally.
    /// </summary>
    public abstract class ValueEnum : Enum
    {
        /// <summary>
        ///     The internal value associated with this <see cref="Enum"/>.
        /// </summary>
        internal object Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueEnum" /> class.
        /// </summary>
        /// <param name="id">The internal ID to associated with this enum.</param>
        /// <param name="value">The internal value to associated with this enum.</param>
        protected ValueEnum(int id, object value)
            : base(id) => Value = value;

        /// <summary>
        ///     Retrieve a string representation of the <see cref="Value"/> of this <see cref="Enum"/>, or an empty string if a string representation is not available.
        /// </summary>
        /// <returns>A string representation of the <see cref="Value"/> of this <see cref="Enum"/>.</returns>
        public override string ToString() => Value.ToString() ?? string.Empty;

        /// <inheritdoc cref="Enum.Equals(object)"/>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            ValueEnum objEnum = (ValueEnum)obj;
            return objEnum.Value.Equals(Value);
        }

#pragma warning disable CA1307
        /// <inheritdoc cref="Enum.GetHashCode()"/>
        public override int GetHashCode() => base.GetHashCode();
#pragma warning restore CA1307
    }
}
