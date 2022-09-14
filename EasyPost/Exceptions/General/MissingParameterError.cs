namespace EasyPost.Exceptions.General
{
    public class MissingParameterError : ValidationError
    {
        internal MissingParameterError(string parameterName) : base($"Missing parameter: {parameterName}")
        {
        }
    }
}
