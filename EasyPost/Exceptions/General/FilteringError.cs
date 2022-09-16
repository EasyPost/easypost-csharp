namespace EasyPost.Exceptions.General
{
    public class FilteringError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FilteringError" /> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        internal FilteringError(string message) : base(message)
        {
        }
    }
}
