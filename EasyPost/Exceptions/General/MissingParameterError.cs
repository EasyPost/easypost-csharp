using System.Globalization;
using System.Reflection;

namespace EasyPost.Exceptions.General
{
    public class MissingParameterError : ValidationError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingParameterError" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the missing parameter.</param>
        internal MissingParameterError(string parameterName)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.MissingRequiredParameter, parameterName))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingParameterError" /> class.
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/> of the missing property.</param>
        internal MissingParameterError(PropertyInfo property)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.MissingRequiredParameter, property.Name))
        {
        }
    }
}
