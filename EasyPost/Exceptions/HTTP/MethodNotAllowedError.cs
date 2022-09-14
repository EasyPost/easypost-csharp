namespace EasyPost.Exceptions
{
    public class MethodNotAllowedError : HttpError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MethodNotAllowedError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        internal MethodNotAllowedError(int statusCode, string errorMessage) : base(statusCode, errorMessage)
        {
        }
    }
}
