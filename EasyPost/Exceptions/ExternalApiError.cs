namespace EasyPost.Exceptions
{
    /// <summary>
    ///     Represents an error that occurs while communicating with an external, non-EasyPost API.
    /// </summary>
    public class ExternalApiError : EasyPostError
    {
        /// <summary>
        ///     Detailed error type string.
        /// </summary>
        public readonly string? Code;

        /// <summary>
        ///     Status code of the HTTP response that caused the error.
        /// </summary>
        public readonly int? StatusCode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExternalApiError" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        internal ExternalApiError(string errorMessage, int statusCode, string? errorType = null)
            : base(errorMessage)
        {
            StatusCode = statusCode;
            Code = errorType;
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the API error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => $@"{Code} ({StatusCode}): {Message}";
    }
}
