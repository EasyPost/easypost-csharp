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
    }
}
