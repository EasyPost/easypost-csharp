namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Represents an error that occurs when there are no more pages of a paginated collection to retrieve from the EasyPost API.
    /// </summary>
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
