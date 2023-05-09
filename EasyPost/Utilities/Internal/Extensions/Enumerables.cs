using System;
using System.Collections.Generic;

namespace EasyPost.Utilities.Internal.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="IEnumerable{T}"/> objects.
    /// </summary>
    public static class Enumerables
    {
        /// <summary>
        ///     Foreach with an index.
        /// </summary>
        /// <param name="ie"><see cref="IEnumerable{T}"/> element to iterate over.</param>
        /// <param name="action">
        ///     <see cref="System.Action"/> to perform for each loop iteration.
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
