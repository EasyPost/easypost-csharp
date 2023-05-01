namespace EasyPost.Exceptions.General
{
#pragma warning disable SA1649
    public class ValidationError : EasyPostError
#pragma warning restore SA1649
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationError" /> class.
        /// </summary>
        /// <param name="message">Error message.</param>
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
