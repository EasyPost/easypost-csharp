using System.Globalization;

namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs due to an invalid function call.
    /// </summary>
    public class InvalidFunctionError : ValidationError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidFunctionError" /> class.
        /// </summary>
        /// <param name="followUpMessage">Additional message to include in error message.</param>
        internal InvalidFunctionError(string? followUpMessage = "")
            : base($"{string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidFunction)} {followUpMessage}")
        {
        }
    }
}
