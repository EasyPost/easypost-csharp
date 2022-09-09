// ReSharper disable once CheckNamespace
using RestSharp;

namespace EasyPost.Exceptions
{
    public class TimeoutError : HttpError
    {
        protected TimeoutError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
