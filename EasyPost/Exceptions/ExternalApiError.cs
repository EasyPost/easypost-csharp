namespace EasyPost.Exceptions
{
    public class ExternalApiError : EasyPostError
    {
        public readonly string? Code;
        public readonly int? StatusCode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExternalApiError" /> class.
        /// </summary>
        /// <param name="errorMessage">Error message string to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        internal ExternalApiError(string errorMessage, int statusCode, string? errorType = null) : base(errorMessage)
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
