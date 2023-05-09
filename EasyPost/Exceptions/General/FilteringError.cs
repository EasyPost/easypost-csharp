﻿namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs while running a filtering operation.
    /// </summary>
    public class FilteringError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FilteringError" /> class.
        /// </summary>
        /// <param name="message">The error message to print to console.</param>
        internal FilteringError(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => Message;
    }
}
