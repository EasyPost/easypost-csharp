using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EasyPost.Utilities.Internal.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="Dictionary{TKey,TValue}"/> objects.
    /// </summary>
    public static class Dictionaries
    {
        /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object? (nullable) key-value pairs to a dictionary of string, object key-value pairs
        ///     by omitting key-value pairs with null values.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object pairs.</returns>
        public static Dictionary<string, object> ToStringNonNullableObjectDictionary(this Dictionary<string, object?> dictionary)
        {
            var newDictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object?> item in dictionary)
            {
                if (item.Value != null)
                {
                    newDictionary.Add(item.Key, item.Value);
                }
            }

            return newDictionary;
        }

        /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object key-value pairs to a dictionary of string, object? (nullable) key-value pairs.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object? pairs.</returns>
        public static Dictionary<string, object?> ToStringNullableObjectDictionary(this Dictionary<string, object> dictionary)
        {
            var newDictionary = new Dictionary<string, object?>();
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                newDictionary.Add(item.Key, item.Value);
            }

            return newDictionary;
        }

        /// <summary>
        ///     Add a key-value pair to a dictionary if the key does not exist, otherwise update the value.
        ///     This is a workaround for the fact that <see cref="IDictionary{TKey,TValue}"/> does not have an AddOrUpdate method.
        ///     This update runs in-place, so the dictionary is not copied and a new dictionary is not returned.
        /// </summary>
        /// <param name="dictionary">The dictionary to add/update the key-value pair in.</param>
        /// <param name="key">The key to add/update.</param>
        /// <param name="value">The value to add/update.</param>
        public static void AddOrUpdate(this IDictionary<string, object?> dictionary, string key, object? value)
        {
            try
            {
                dictionary.Add(key, value);
            }
            catch (ArgumentException)
            {
                dictionary[key] = value;
            }
        }

        /// <summary>
        ///     Wrap a dictionary into a larger dictionary.
        ///     i.e. add a dictionary of parameters to "level1" -> "level2" -> "level3" -> "parameters".
        /// </summary>
        /// <param name="dictionary">Dictionary to wrap.</param>
        /// <param name="keys">Path of keys to wrap the parameters in.</param>
        /// <returns>A wrapped dictionary.</returns>
        internal static Dictionary<string, object> Wrap(this Dictionary<string, object> dictionary, params string[] keys) => keys.Reverse()
            .Aggregate(dictionary, (current, key) => new Dictionary<string, object> { { key, current } });

        /// <summary>
        ///     Wrap a list into a larger dictionary.
        ///     i.e. add a list of parameters to "level1" -> "level2" -> "level3" -> "parameters".
        /// </summary>
        /// <param name="list">List to wrap.</param>
        /// <param name="keys">Path of keys to wrap the parameters in.</param>
        /// <typeparam name="T">Type of list elements.</typeparam>
        /// <returns>A wrapped dictionary.</returns>
        internal static Dictionary<string, object> Wrap<T>(this List<T> list, params string[] keys)
        {
            string firstKey = keys.Reverse().First();
            Dictionary<string, object> dictionary = new() { { firstKey, list } };
            return keys.Reverse().Skip(1).Aggregate(dictionary, (current, key) => new Dictionary<string, object> { { key, current } });
        }

        /// <summary>
        ///     Get a value from a dictionary, or <c>null</c> if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <typeparam name="T">The type of value to extract.</typeparam>
        /// <returns>A T-type object, or <c>null</c> if key does not exist.</returns>
        internal static T? GetOrNull<T>(this Dictionary<string, object> dictionary, string key) where T : class
        {
            if (!dictionary.TryGetValue(key, out object? value)) return null;
            return value switch
            {
                T t => t,
                JObject jObject => jObject.ToObject<T>(),
                JArray jArray => jArray.ToObject<T>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a value from a dictionary, or the default if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <typeparam name="T">The type of value to extract.</typeparam>
        /// <returns>A T-type object, or default if key does not exist.</returns>
        internal static T? GetOrDefault<T>(this Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out object? value)) return default;
            return value switch
            {
                T t => t,
                JObject jObject => jObject.ToObject<T>(),
                JArray jArray => jArray.ToObject<T>(),
                var _ => default,
            };
        }

        /// <summary>
        ///     Get a boolean value from a dictionary, or <c>null</c> if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A boolean, or <c>null</c> if key does not exist.</returns>
        internal static bool? GetOrNullBoolean(this Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out object? value)) return null;
            return value switch
            {
                bool b => b,
                JObject jObject => jObject.ToObject<bool>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a double value from a dictionary, or <c>null</c> if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A double, or <c>null</c> if key does not exist.</returns>
        internal static double? GetOrNullDouble(this Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out object? value)) return null;
            return value switch
            {
                double d => d,
                float f => (double)f,
                int i => (double)i,
                long l => (double)l,
                string s => double.TryParse(s, out double d) ? d : null,
                JObject jObject => jObject.ToObject<double>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get an int value from a dictionary, or <c>null</c> if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>An int, or <c>null</c> if key does not exist.</returns>
        internal static int? GetOrNullInt(this Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out object? value)) return null;
            return value switch
            {
                int i => i,
                long l => (int)l,
                float f => (int)f,
                double d => (int)d,
                string s => int.TryParse(s, out int i) ? i : null,
                JObject jObject => jObject.ToObject<int>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a float value from a dictionary, or <c>null</c> if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A float, or <c>null</c> if key does not exist.</returns>
        internal static float? GetOrNullFloat(this Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out object? value)) return null;
            return value switch
            {
                float f => f,
                double d => (float)d,
                int i => (float)i,
                long l => (float)l,
                string s => float.TryParse(s, out float f) ? f : null,
                JObject jObject => jObject.ToObject<float>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a long value from a dictionary, or <c>null</c> if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A long, or <c>null</c> if key does not exist.</returns>
        internal static long? GetOrNullLong(this Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out object? value)) return null;
            return value switch
            {
                long l => l,
                int i => (long)i,
                float f => (long)f,
                double d => (long)d,
                string s => long.TryParse(s, out long l) ? l : null,
                JObject jObject => jObject.ToObject<long>(),
                var _ => null,
            };
        }
    }
}
