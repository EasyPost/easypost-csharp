// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class ProxyError : HttpError
    {
        protected ProxyError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
