namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs due to an invalid object.
    /// </summary>
    public class InvalidObjectError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidObjectError" /> class.
        /// </summary>
        /// <param name="message">The error message to print to console.</param>
        internal InvalidObjectError(string message)
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
