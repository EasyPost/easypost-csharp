using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Internal
{
#pragma warning disable SA1649
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
        private int Id { get; }

        protected Enum(int id) => Id = id;

        public int CompareTo(object? obj) => Id.CompareTo(((Enum)obj!).Id);

        public override string ToString() => Id.ToString(CultureInfo.InvariantCulture);

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
        public override int GetHashCode() => new Dictionary<string, int> { { GetType().ToString(), Id } }.GetHashCode();
#pragma warning restore CA1307

        private bool Equals(Enum? other) => Id == other?.Id;

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

        public static bool operator !=(Enum? one, Enum? two) => !(one == two);

        public static IEnumerable<T> GetAll<T>()
            where T : IEnum
            =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public static bool operator <(Enum left, Enum right) => left.CompareTo(right) < 0;

        public static bool operator <=(Enum left, Enum right) => left.CompareTo(right) <= 0;

        public static bool operator >(Enum left, Enum right) => left.CompareTo(right) > 0;

        public static bool operator >=(Enum left, Enum right) => left.CompareTo(right) >= 0;
    }

    /// <summary>
    ///     An enum that stores a value internally.
    /// </summary>
    public abstract class ValueEnum : Enum
    {
        internal object Value { get; }

        protected ValueEnum(int id, object value)
            : base(id) => Value = value;

        public override string ToString() => Value.ToString() ?? string.Empty;

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
        public override int GetHashCode() => base.GetHashCode();
#pragma warning restore CA1307
    }
}
