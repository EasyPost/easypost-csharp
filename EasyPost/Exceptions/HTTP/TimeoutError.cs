// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class TimeoutError : HttpError
    {
        protected TimeoutError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
