using System.Collections.Generic;

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
    }
}
