using System;

namespace EasyPost.Exceptions
{
    public abstract class EasyPostError : Exception
    {
        internal EasyPostError(string message) : base(message)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the EasyPost API error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public abstract string PrettyPrint { get; }
    }
}
