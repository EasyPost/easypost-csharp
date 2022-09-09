// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class SignatureVerificationError : EasyPostError
    {
        public SignatureVerificationError() : base("Invalid signature") { }
    }
}
