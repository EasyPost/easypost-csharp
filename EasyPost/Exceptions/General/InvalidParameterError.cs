using System.Globalization;

namespace EasyPost.Exceptions.General
{
    public class InvalidParameterError : ValidationError
    {
        internal InvalidParameterError(string parameterName)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidParameter, parameterName))
        {
        }
    }
}
