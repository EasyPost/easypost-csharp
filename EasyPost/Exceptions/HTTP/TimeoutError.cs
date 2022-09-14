﻿// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class TimeoutError : HttpError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeoutError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        internal TimeoutError(int statusCode, string errorMessage) : base(statusCode, errorMessage)
        {
        }
    }
}
