// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class ConnectionError : HttpError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConnectionError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        internal ConnectionError(int statusCode, string errorMessage) : base(statusCode, errorMessage)
        {
        }
    }
}
