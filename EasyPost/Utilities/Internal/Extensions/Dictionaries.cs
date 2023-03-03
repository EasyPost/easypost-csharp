using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Utilities.Internal.Extensions
{
    public static class Dictionaries
    {
        /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object? (nullable) key-value pairs to a dictionary of string, object key-value pairs
        ///     by omitting key-value pairs with null values.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object pairs.</returns>
        public static Dictionary<string, object> ConvertToStringNonNullableObjectDictionary(Dictionary<string, object?> dictionary)
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
        public static Dictionary<string, object?> ConvertToStringNullableObjectDictionary(Dictionary<string, object> dictionary)
        {
            var newDictionary = new Dictionary<string, object?>();
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                newDictionary.Add(item.Key, item.Value);
            }

            return newDictionary;
        }

        /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object? (nullable) key-value pairs to a dictionary of string, object key-value pairs
        ///     by omitting key-value pairs with null values.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object pairs.</returns>
        public static Dictionary<string, object> ToStringNonNullableObjectDictionary(this Dictionary<string, object?> dictionary) => ConvertToStringNonNullableObjectDictionary(dictionary);

        /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object key-value pairs to a dictionary of string, object? (nullable) key-value pairs.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object? pairs.</returns>
        public static Dictionary<string, object?> ToStringNullableObjectDictionary(this Dictionary<string, object> dictionary) => ConvertToStringNullableObjectDictionary(dictionary);

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
    }
}
