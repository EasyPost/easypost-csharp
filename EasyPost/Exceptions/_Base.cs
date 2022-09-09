using System;
using RestSharp;

namespace EasyPost.Exceptions
{
    public class EasyPostError : Exception
    {
        protected EasyPostError(string message) : base(message)
        {
        }
    }

    public abstract class GenerateFromResponse : EasyPostError
    {
        protected GenerateFromResponse(string message) : base(message)
        {
        }

        // great minds think alike: https://github.com/stripe/stripe-dotnet/blob/6b9513d3b938d265c7607db919ad2c536ab578c3/src/Stripe.net/Infrastructure/Public/StripeClient.cs#L171

        public static T FromResponse<T>(RestResponse response) where T : GenerateFromResponse
        {
            // not implemented, to be override by subclasses
            return null;
        }
    }
}
