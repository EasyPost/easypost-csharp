using System;

namespace EasyPost.Tests._Utilities.Assertions
{
    // ReSharper disable once PartialTypeWithSinglePart
    public abstract partial class Assert
    {
        /// <summary>
        ///     Verifies that an action does not throw an exception.
        /// </summary>
        /// <param name="action">The action to test</param>
        /// <exception cref="DoesNotThrowException">Thrown when the action throws an exception</exception>
        public static void DoesNotThrow(Action action)
        {
            GuardArgumentNotNull(nameof(action), action);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new DoesNotThrowException(ex);
            }
        }

        /// <summary>
        ///     Verifies that an action does not throw a specific exception.
        /// </summary>
        /// <param name="action">The action to test</param>
        /// <typeparam name="T">The type of the exception to not throw</typeparam>
        /// <exception cref="DoesNotThrowException">Thrown when the action throws an exception of type T</exception>
        public static void DoesNotThrow<T>(Action action) where T : Exception
        {
            GuardArgumentNotNull(nameof(action), action);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(T))
                {
                    throw new DoesNotThrowException(ex);
                }
            }
        }
    }
}
