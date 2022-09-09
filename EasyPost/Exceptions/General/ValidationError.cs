// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class ValidationError : EasyPostError
    {
        protected ValidationError(string message) : base(message)
        {
        }
    }
}
