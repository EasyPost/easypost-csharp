namespace EasyPost.Exceptions.General
{
    public class InvalidObjectError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidObjectError" /> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        public InvalidObjectError(string message) : base(message)
        {
        }
    }
}
