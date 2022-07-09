using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities
{
    public abstract class Enum : IComparable
    {
        private int Id { get; }

        protected Enum(int id)
        {
            Id = id;
        }

        public int CompareTo(object? other) => Id.CompareTo(((Enum)other!).Id);

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
            catch (Exception)
            {
                // casting likely failed
                return false;
            }
        }

        public override int GetHashCode()
        {
            {
                return new Dictionary<string, int>
                {
                    {
                        GetType().ToString(), Id
                    }
                }.GetHashCode();
            }
        }

        private bool Equals(Enum? other) => Id == other?.Id;

        public static bool operator ==(Enum? one, Enum? two)
        {
            if (one is null || two is null)
            {
                return false;
            }

            return one.Equals(two);
        }

        public static bool operator !=(Enum? one, Enum? two)
        {
            if (one is null || two is null)
            {
                return false;
            }

            return !(one == two);
        }

        protected static IEnumerable<T> GetAll<T>() where T : Enum =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
    }

    public abstract class ValueEnum : Enum
    {
        internal object Value { get; }

        protected ValueEnum(int id, object value) : base(id)
        {
            Value = value;
        }
    }

    public abstract class MultiValueEnum : Enum
    {
        internal object[] Values { get; }

        protected MultiValueEnum(int id, params object[] values) : base(id)
        {
            Values = values;
        }
    }
}
