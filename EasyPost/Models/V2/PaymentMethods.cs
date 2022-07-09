using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    /// <summary>
    ///     Represents a summary of the primary and secondary payment methods on the user's account.
    /// </summary>
    public class PaymentMethodsSummary : EasyPostObject
    {
        [JsonProperty("primary_payment_method")]
        public PaymentMethod? PrimaryPaymentMethod { get; set; }
        [JsonProperty("secondary_payment_method")]
        public PaymentMethod? SecondaryPaymentMethod { get; set; }
    }

    /// <summary>
    ///     Represents a credit card or a bank account.
    ///     Have to collect all possible properties since we can't anticipate which type of payment method the API will return.
    /// </summary>
    public class PaymentMethod : EasyPostObject
    {
        internal string Endpoint
        {
            get
            {
                if (Type == null)
                {
                    throw new Exception("Unknown payment method type");
                }

                return Type.EndPoint;
            }
        }

        /// <summary>
        ///     Get what type of payment method this is (credit card, bank account, etc.)
        /// </summary>
        private PaymentMethodType? Type
        {
            get
            {
                switch (Prefix)
                {
                    case "card":
                        return PaymentMethodType.CreditCard;
                    case "bank":
                        return PaymentMethodType.BankAccount;
                    default:
                        return null;
                }
            }
        }

        #region JSON Properties

        // bank_account
        [JsonProperty("bank_name")]
        public string? BankName { get; set; }
        // credit_card
        [JsonProperty("brand")]
        public string? Brand { get; set; }
        // bank_account
        [JsonProperty("country")]
        public string? Country { get; set; }
        // both
        [JsonProperty("disabled_at")]
        public string? Disabled { get; set; }
        // credit_card
        [JsonProperty("exp_month")]
        public string? ExpirationMonth { get; set; }
        // credit_card
        [JsonProperty("exp_year")]
        public string? ExpirationYear { get; set; }
        // both
        [JsonProperty("last4")]
        public string? LastFour { get; set; }
        // credit_card
        [JsonProperty("name")]
        public string? Name { get; set; }
        // bank_account
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        #endregion JSON Properties
    }
}
