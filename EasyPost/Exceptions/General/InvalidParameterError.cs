using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs due to an invalid parameter.
    /// </summary>
    [SuppressMessage("Performance", "CA1863:Use \'CompositeFormat\'")]
    public class InvalidParameterError : ValidationError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidParameterError" /> class.
        /// </summary>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <param name="followUpMessage">Additional message to include in error message.</param>
        internal InvalidParameterError(string parameterName, string? followUpMessage = "")
            : base($"{string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidParameter, parameterName)}. {followUpMessage}")
        {
        }
    }
}
