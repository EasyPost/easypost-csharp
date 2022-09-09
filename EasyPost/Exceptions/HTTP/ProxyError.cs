// ReSharper disable once CheckNamespace
using RestSharp;

namespace EasyPost.Exceptions
{
    public class ProxyError : HttpError
    {
        protected ProxyError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
