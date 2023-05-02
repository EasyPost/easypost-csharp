using System.Globalization;

namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs due to an invalid parameter.
    /// </summary>
    public class InvalidParameterError : ValidationError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidParameterError" /> class.
        /// </summary>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        internal InvalidParameterError(string parameterName)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidParameter, parameterName))
        {
        }
    }
}
