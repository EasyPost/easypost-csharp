namespace EasyPost.Exceptions.General
{
    public class InvalidParameterError : ValidationError
    {
        internal InvalidParameterError(string parameterName) : base(string.Format(Constants.ErrorMessages.InvalidParameter, parameterName))
        {
        }
    }
}
