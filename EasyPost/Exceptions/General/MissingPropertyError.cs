namespace EasyPost.Exceptions.General
{
    public class MissingPropertyError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingPropertyError" /> class.
        /// </summary>
        /// <param name="obj">Object missing the property.</param>
        /// <param name="propertyName">Name of the missing property.</param>
        internal MissingPropertyError(object obj, string propertyName) : base(string.Format(Constants.ErrorMessages.MissingProperty, new object[] { obj.GetType().Name, propertyName }))
        {
        }
    }
}
