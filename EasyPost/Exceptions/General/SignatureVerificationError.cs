namespace EasyPost.Exceptions.General
{
    public class SignatureVerificationError : EasyPostError
    {
        internal SignatureVerificationError() : base("Invalid signature")
        {
        }
    }
}
