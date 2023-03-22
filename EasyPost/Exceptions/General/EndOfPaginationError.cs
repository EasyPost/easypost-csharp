namespace EasyPost.Exceptions.General
{
    public class EndOfPaginationError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EndOfPaginationError" /> class.
        /// </summary>
        internal EndOfPaginationError()
            : base("There are no more pages to retrieve.")
        {
        }
    }
}
