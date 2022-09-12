// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class RetryError : HttpError
    {
        protected RetryError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
