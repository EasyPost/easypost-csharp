using System.Globalization;

namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs due to an invalid parameter pair.
    /// </summary>
    public class InvalidParameterPairError : ValidationError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidParameterPairError" /> class.
        /// </summary>
        /// <param name="firstParameterName">The name of the first parameter in the pair.</param>
        /// <param name="secondParameterName">The name of the second parameter in the pair.</param>
        /// <param name="followUpMessage">Additional message to include in error message.</param>
        internal InvalidParameterPairError(string firstParameterName, string secondParameterName, string? followUpMessage = "")
            : base($"{string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.InvalidParameterPair, firstParameterName, secondParameterName)}. {followUpMessage}")
        {
        }
    }
}
