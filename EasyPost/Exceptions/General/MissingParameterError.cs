namespace EasyPost.Exceptions.General
{
    public class MissingParameterError : ValidationError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingParameterError" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the missing parameter.</param>
        internal MissingParameterError(string parameterName) : base(string.Format(Constants.ErrorMessages.MissingRequiredParameter, parameterName))
        {
        }
    }
}
