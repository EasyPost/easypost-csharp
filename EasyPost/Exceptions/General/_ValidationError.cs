namespace EasyPost.Exceptions.General;

public class ValidationError : EasyPostError
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ValidationError" /> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    protected ValidationError(string message) : base(message)
    {
    }
}
