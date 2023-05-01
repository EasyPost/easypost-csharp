namespace EasyPost.Exceptions.General
{
    public class EndOfPaginationError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EndOfPaginationError" /> class.
        /// </summary>
        internal EndOfPaginationError()
            : base(Constants.ErrorMessages.NoMorePagesToRetrieve)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => Message;
    }
}
