using System.Globalization;

namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs due to a missing property on an object.
    /// </summary>
    public class MissingPropertyError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingPropertyError" /> class.
        /// </summary>
        /// <param name="obj">Object missing the property.</param>
        /// <param name="propertyName">Name of the missing property.</param>
#pragma warning disable CA2241
#pragma warning disable IDE0300
        internal MissingPropertyError(object obj, object propertyName)
            // ReSharper disable once UseCollectionExpression
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.MissingProperty, new[] { obj.GetType().Name, propertyName }))
#pragma warning restore IDE0300
#pragma warning restore CA2241
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => Message;
    }
}
