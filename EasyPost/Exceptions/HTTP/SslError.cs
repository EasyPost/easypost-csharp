// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class SslError : HttpError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SslError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        internal SslError(int statusCode, string errorMessage) : base(statusCode, errorMessage)
        {
        }
    }
}
