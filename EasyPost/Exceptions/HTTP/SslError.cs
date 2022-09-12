// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class SslError : HttpError
    {
        protected SslError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
