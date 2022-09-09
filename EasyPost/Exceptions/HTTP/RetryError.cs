// ReSharper disable once CheckNamespace
using RestSharp;

namespace EasyPost.Exceptions
{
    public class RetryError : HttpError
    {
        protected RetryError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
