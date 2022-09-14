namespace EasyPost.Exceptions.General
{
    public class InvalidParameterError : ValidationError
    {
        internal InvalidParameterError(string parameterName) : base($"Invalid parameter: {parameterName}")
        {
        }
    }
}
