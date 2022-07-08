using System;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost
{
    /// <summary>
    ///     Represents a summary of the primary and secondary payment methods on the user's account.
    /// </summary>
    public class PaymentMethod : Resource
    {
        /// <summary>
        ///     Payment method priority
        /// </summary>
        public enum Priority
        {
            Primary,
            Secondary
        }

        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("primary_payment_method")]
        public PaymentMethodObject primary_payment_method { get; set; }
        [JsonProperty("secondary_payment_method")]
        public PaymentMethodObject secondary_payment_method { get; set; }
    }

    /// <summary>
    ///     Represents a credit card or a bank account.
    ///     Have to collect all possible properties since we can't anticipate which type of payment method the API will return.
    /// </summary>
    public class PaymentMethodObject : Resource
    {
        /// <summary>
        ///     Get what type of payment method this is (credit card, bank account, etc.)
        /// </summary>
        public PaymentMethodType? Type
        {
            get
            {
                PaymentMethodType? type = null;
                if (id == null)
                {
                    return null;
                }

                if (id.StartsWith("card_"))
                {
                    type = PaymentMethodType.CreditCard;
                }

                else if (id.StartsWith("bank_"))
                {
                    type = PaymentMethodType.BankAccount;
                }

                return type;
            }
        }

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

        public class PaymentMethodType : Enumeration
        {
            public static readonly PaymentMethodType BankAccount = new PaymentMethodType(2, "bank_accounts");
            public static readonly PaymentMethodType CreditCard = new PaymentMethodType(1, "credit_cards");

            internal string EndPoint => Name;

            private PaymentMethodType(int id, string name) : base(id, name)
            {
            }
        }

        #region JSON Properties

        // bank_account
        [JsonProperty("bank_name")]
        public string bank_name { get; set; }
        // credit_card
        [JsonProperty("brand")]
        public string brand { get; set; }
        // bank_account
        [JsonProperty("country")]
        public string country { get; set; }
        // both
        [JsonProperty("disabled_at")]
        public string disabled_at { get; set; }
        // credit_card
        [JsonProperty("exp_month")]
        public int exp_month { get; set; }
        // credit_card
        [JsonProperty("exp_year")]
        public int exp_year { get; set; }
        // both
        [JsonProperty("id")]
        public string id { get; set; }
        // both
        [JsonProperty("last4")]
        public string last4 { get; set; }
        // credit_card
        [JsonProperty("name")]
        public string name { get; set; }
        // both
        [JsonProperty("object")]
        public string Object { get; set; }
        // bank_account
        [JsonProperty("verified")]
        public bool verified { get; set; }

        #endregion JSON Properties
    }
}
