using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Http
{
    public static class Parameters
    {
        /// <summary>
        ///     Wrap a dictionary into a larger dictionary.
        ///     i.e. add a dictionary of parameters to "level1" -> "level2" -> "level3" -> "parameters"
        /// </summary>
        /// <param name="dictionary">Dictionary to wrap.</param>
        /// <param name="keys">Path of keys to wrap the parameters in.</param>
        /// <returns>A wrapped dictionary.</returns>
        internal static Dictionary<string, object> Wrap(this Dictionary<string, object> dictionary, params string[] keys) => keys.Reverse()
                .Aggregate(dictionary, (current, key) => new Dictionary<string, object> { { key, current } });

        /// <summary>
        ///     Wrap a list into a larger dictionary.
        ///     i.e. add a list of parameters to "level1" -> "level2" -> "level3" -> "parameters"
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
