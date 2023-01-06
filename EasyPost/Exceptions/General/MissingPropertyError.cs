using System.Globalization;

namespace EasyPost.Exceptions.General
{
    public class MissingPropertyError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingPropertyError" /> class.
        /// </summary>
        /// <param name="obj">Object missing the property.</param>
        /// <param name="propertyName">Name of the missing property.</param>
#pragma warning disable CA2241
        internal MissingPropertyError(object obj, object propertyName)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.MissingProperty, new[] { obj.GetType().Name, propertyName }))
#pragma warning restore CA2241
        {
        }
    }
}
