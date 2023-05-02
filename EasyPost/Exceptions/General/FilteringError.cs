namespace EasyPost.Exceptions.General
{
    public class FilteringError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FilteringError" /> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        internal FilteringError(string message)
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
