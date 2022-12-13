namespace EasyPost.Exceptions.General
{
    public class SignatureVerificationError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SignatureVerificationError" /> class.
        /// </summary>
        internal SignatureVerificationError() : base(Constants.ErrorMessages.InvalidWebhookSignature)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => Message;
    }
}
