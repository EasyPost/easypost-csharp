// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class InvalidParameterError : ValidationError
    {
        public InvalidParameterError(string parameterName) : base($"Invalid parameter: {parameterName}")
        {
        }
    }
}
