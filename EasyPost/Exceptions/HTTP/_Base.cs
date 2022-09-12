using RestSharp;

// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class HttpError : GenerateFromResponse
    {
        public string? ErrorMessage;
        public int StatusCode;

        // All constructors for HTTP exceptions are protected, so you cannot directly initialize an instance of the exception class.
        // Instead, you must use the .FromResponse method to retrieve an instance.

        protected HttpError(int statusCode, string? errorMessage = null) : base(errorMessage ?? $"HTTP error {statusCode}") // this fallback should never occur
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public static HttpError DetermineError(RestResponse response)
        {
            // TODO: Logic here to determine which type of HTTP error to return
            return FromResponse<HttpError>(response);
        }

        public static new T FromResponse<T>(RestResponse response) where T : HttpError
        {
            int statusCode = (int)response.StatusCode;
            string? errorMessage = response.ErrorMessage;

            var error = new HttpError(statusCode, errorMessage);
            return error as T;
        }
    }
}
