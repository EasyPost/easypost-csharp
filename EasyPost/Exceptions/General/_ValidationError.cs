namespace EasyPost.Exceptions.General
{
#pragma warning disable SA1649
    /// <summary>
    ///     Base class for all validation errors.
    /// </summary>
    public abstract class ValidationError : EasyPostError
#pragma warning restore SA1649
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationError" /> class.
        /// </summary>
        /// <param name="message">The error message to print to console.</param>
        protected ValidationError(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => Message;
    }
}
