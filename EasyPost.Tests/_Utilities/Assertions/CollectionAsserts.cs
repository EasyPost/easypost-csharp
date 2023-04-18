using System;
using System.Collections.Generic;

namespace EasyPost.Tests._Utilities.Assertions
{
    public abstract partial class Assert
    {
        private static void GuardArgumentNotNull(string argName, object argValue)
        {
            if (argValue == null)
                throw new ArgumentNullException(argName);
        }

        /// <summary>
        /// Verifies that any items in the collection pass when executed against
        /// action.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <exception cref="AnyException">Thrown when the collection contains no matching element</exception>
        public static void Any<T>(IEnumerable<T> collection, Action<T> action)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            Any(collection, (item, index) => action(item));
        }

        /// <summary>
        /// Verifies that any items in the collection pass when executed against
        /// action. The item index is provided to the action, in addition to the item.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <exception cref="AnyException">Thrown when the collection contains no matching element</exception>
        public static void Any<T>(IEnumerable<T> collection, Action<T, int> action)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            int idx = 0;

            bool passed = false;

            foreach (var item in collection)
            {
                try
                {
                    action(item, idx);
                    passed = true;
                    break; // if we get here, we passed, so we can stop iterating
                }
                catch (Exception ex)
                {
                    // we don't care about the exception, we just want to keep iterating
                }

                ++idx;
            }

            if (!passed)
                throw new AnyException();
        }
    }
}
