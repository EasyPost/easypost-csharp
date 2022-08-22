using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Http
{
    public static class Parameters
    {
        internal static Dictionary<string, object> Wrap(this Dictionary<string, object> parameters, params string[] keys)
        {
            return keys.Reverse()
                .Aggregate(parameters, (current, key) => new Dictionary<string, object>
                {
                    {
                        key, current
                    }
                });
        }

        internal static Dictionary<string, object> Wrap<T>(this List<T> parameters, params string[] keys)
        {
            string firstKey = keys.Reverse().First();
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {
                    firstKey, parameters
                }
            };
            return keys.Reverse().Skip(1).Aggregate(dictionary, (current, key) => new Dictionary<string, object>
            {
                {
                    key, current
                }
            });
        }
    }
}
