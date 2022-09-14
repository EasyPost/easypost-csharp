

// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class HttpError : EasyPostError
    {
        public readonly int StatusCode;

        // All constructors for HTTP exceptions are protected, so you cannot directly initialize an instance of the exception class.
        // Instead, you must use the .FromResponse method to retrieve an instance.

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        protected HttpError(int statusCode, string errorMessage) : base(errorMessage)
        {
            StatusCode = statusCode;
        }
    }
}
