// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class ConnectionError : HttpError
    {
        protected ConnectionError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
