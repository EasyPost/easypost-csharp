using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        internal int Id { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enum" /> class.
        /// </summary>
        /// <param name="id">The internal ID to associated with this enum.</param>
        protected Enum(int id) => Id = id;

        /// <inheritdoc cref="Int32.CompareTo(int)"/>
        public int CompareTo(object? obj) => Id.CompareTo(((Enum)obj!).Id);

        /// <inheritdoc cref="Int32.ToString()"/>
        public override string ToString() => Id.ToString(CultureInfo.InvariantCulture);

        /// <inheritdoc cref="System.Object.Equals(object?)"/>
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
        /// <inheritdoc cref="System.Object.GetHashCode()"/>
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

        /// <summary>
        ///     Get the <see cref="Enum"/> associated with the given ID.
        /// </summary>
        /// <param name="value">ID to determine <see cref="Enum"/> from.</param>
        /// <typeparam name="T">Type of <see cref="Enum"/> to return.</typeparam>
        /// <returns>A T-type enum corresponding to the provided value, or null.</returns>
        public static T? FromValue<T>(object? value) where T : Enum
        {
            return GetAll<T>().FirstOrDefault(enumValue => enumValue.Id.Equals(value));
        }
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
        ///     A string representation of the <see cref="Value"/> of this <see cref="Enum"/>, or an empty string if a string representation is not available.
        /// </summary>
        public override string ToString() => Value.ToString() ?? string.Empty;

        /// <summary>
        ///     Get the <see cref="ValueEnum"/> associated with the given value.
        /// </summary>
        /// <param name="value">Value to determine <see cref="ValueEnum"/> from.</param>
        /// <typeparam name="T">Type of <see cref="ValueEnum"/> to return.</typeparam>
        /// <returns>A T-type enum corresponding to the provided value, or null.</returns>
        public static new T? FromValue<T>(object? value) where T : ValueEnum
        {
            return GetAll<T>().FirstOrDefault(@enum => @enum.Value.Equals(value));
        }

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

/// <summary>
///     A <see cref="JsonConverter"/> for <see cref="EasyPost.Utilities.Internal.Enum"/>s.
/// </summary>
/// <typeparam name="T">The <see cref="EasyPost.Utilities.Internal.Enum"/> sub-type to de/serialize.</typeparam>
internal class EnumJsonConverter<T> : JsonConverter<EasyPost.Utilities.Internal.Enum> where T : EasyPost.Utilities.Internal.Enum
{
    public override void WriteJson(JsonWriter writer, EasyPost.Utilities.Internal.Enum? value, JsonSerializer serializer)
    {
        int? enumValue = value?.Id;
        if (enumValue is null)
        {
            writer.WriteNull();
            return;
        }

        serializer.Serialize(writer, enumValue);
    }

    public override EasyPost.Utilities.Internal.Enum? ReadJson(JsonReader reader, Type objectType, EasyPost.Utilities.Internal.Enum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        JToken jToken = JToken.Load(reader);

        // Extract the value from the JToken
        object? enumId = jToken.Value<object>();
        if (enumId is JValue jValue)
            enumId = jValue.Value;

        // Find corresponding enum based on the value
        T? target = EasyPost.Utilities.Internal.Enum.FromValue<T>(enumId);

        return target ?? null;
    }
}

/// <summary>
///     A <see cref="JsonConverter"/> for <see cref="ValueEnum"/>s.
/// </summary>
/// <typeparam name="T">The <see cref="ValueEnum"/> sub-type to de/serialize.</typeparam>
internal class ValueEnumJsonConverter<T> : JsonConverter<ValueEnum> where T : ValueEnum
{
    public override void WriteJson(JsonWriter writer, ValueEnum? value, JsonSerializer serializer)
    {
        object? enumValue = value?.Value;
        if (enumValue is null)
        {
            writer.WriteNull();
            return;
        }

        serializer.Serialize(writer, enumValue);
    }

    public override ValueEnum? ReadJson(JsonReader reader, Type objectType, ValueEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        JToken jToken = JToken.Load(reader);

        // Extract the value from the JToken
        object? enumValue = jToken.Value<object>();
        if (enumValue is JValue jValue)
            enumValue = jValue.Value;

        // Find corresponding enum based on the value
        T? target = ValueEnum.FromValue<T>(enumValue);

        return target ?? null;
    }
}
