using System;

namespace EasyPost.Exceptions
{
#pragma warning disable SA1649
    /// <summary>
    ///     Base class for all EasyPost exceptions.
    /// </summary>
    public abstract class EasyPostError : Exception
#pragma warning restore SA1649
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EasyPostError" /> class.
        /// </summary>
        /// <param name="message">The error message to print to console.</param>
        internal EasyPostError(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the EasyPost API error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public abstract string PrettyPrint { get; }
    }
}
