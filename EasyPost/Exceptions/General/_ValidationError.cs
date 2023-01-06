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
    }
}
