namespace EasyPost.Exceptions.General;

public class SignatureVerificationError : EasyPostError
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SignatureVerificationError" /> class.
    /// </summary>
    internal SignatureVerificationError() : base(Constants.ErrorMessages.InvalidWebhookSignature)
    {
    }
}
