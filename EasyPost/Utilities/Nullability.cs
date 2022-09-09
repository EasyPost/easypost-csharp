using System.Linq;

namespace EasyPost.Utilities
{
    internal static class Nullability
    {
        /// <summary>
        ///     Check if any of the provided elements are null.
        /// </summary>
        /// <param name="elements">Elements to check if they are null.</param>
        /// <returns>True if any of the elements are null, False otherwise.</returns>
        internal static bool AnyAreNull(params object?[] elements)
        {
            return elements.Any(element => element == null);
        }
    }
}
