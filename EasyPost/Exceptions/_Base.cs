using System;

namespace EasyPost.Exceptions
{
#pragma warning disable SA1649
    public abstract class EasyPostError : Exception
#pragma warning restore SA1649
    {
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
