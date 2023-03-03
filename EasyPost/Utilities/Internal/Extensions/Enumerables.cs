using System;
using System.Collections.Generic;

namespace EasyPost.Utilities.Internal.Extensions
{
    public static class Enumerables
    {
        /// <summary>Foreach with an index.</summary>
        /// <param name="ie">IEnumerable element.</param>
        /// <param name="action">
        ///     Action to do for each loop.
        ///     Action receives a (int, T) tuple containing the index and the current element in the loop as a parameter
        /// </param>
        /// <typeparam name="T">Type of input.</typeparam>
        public static void Each<T>(this IEnumerable<T> ie, Action<int, T> action)
        {
            int num = 0;
            foreach (T obj in ie)
            {
                action(num++, obj);
            }
        }
    }
}
