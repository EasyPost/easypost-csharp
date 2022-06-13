using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities
{
    public abstract class Enumeration : IComparable
    {
        internal int Id { get; private set; }
        internal string Name { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public int CompareTo(object? other) => other == null ? 1 : Id.CompareTo(((Enumeration)other).Id);

        public override bool Equals(object? obj)
        {
            try
            {
                if (!GetType().Equals(obj.GetType()))
                {
                    // types are not the same
                    return false;
                }

                Enumeration objEnum = (Enumeration)obj;
                return objEnum.Id == Id && objEnum.Name == Name;
            }
            catch (Exception)
            {
                // casting likely failed
                return false;
            }
        }

        public override string ToString() => Name;

        protected static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
    }
}
