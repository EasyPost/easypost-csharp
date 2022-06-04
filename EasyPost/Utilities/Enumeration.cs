using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities
{
    public abstract class Enumeration : IComparable
    {
        private int Id { get; }
        internal string Name { get; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public int CompareTo(object? other) => Id.CompareTo(((Enumeration)other!).Id);

        protected bool Equals(Enumeration other) => Id == other.Id && Name == other.Name;

        public override bool Equals(object? obj)
        {
            try
            {
                if (GetType() != obj!.GetType())
                {
                    // types are not the same
                    return false;
                }

                Enumeration objEnum = (Enumeration)obj;
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
            unchecked
            {
                return (Id * 397) ^ Name.GetHashCode();
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
