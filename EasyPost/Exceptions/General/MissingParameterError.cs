// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class MissingParameterError : ValidationError
    {
        public MissingParameterError(string parameterName) : base($"Missing parameter: {parameterName}")
        {
        }
    }
}
