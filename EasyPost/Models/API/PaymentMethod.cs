using EasyPost._base;
using EasyPost.Exceptions;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Represents a credit card or a bank account.
    ///     Have to collect all possible properties since we can't anticipate which type of payment method the API will return.
    /// </summary>
    public class PaymentMethod : EasyPostObject
    {
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
        public string? DisabledAt { get; set; }
        // credit_card
        [JsonProperty("exp_month")]
        public int? ExpMonth { get; set; }
        // credit_card
        [JsonProperty("exp_year")]
        public int? ExpYear { get; set; }
        // both
        [JsonProperty("last4")]
        public string? Last4 { get; set; }
        // credit_card
        [JsonProperty("name")]
        public string? Name { get; set; }
        // bank_account
        [JsonProperty("verified")]
        public bool? Verified { get; set; }

        #endregion

        /// <summary>
        ///     Payment method priority
        /// </summary>
        public enum Priority
        {
            Primary,
            Secondary
        }

        /// <summary>
        ///     Get what type of payment method this is (credit card, bank account, etc.)
        /// </summary>
        public PaymentMethodType? Type
        {
            get
            {
                PaymentMethodType? type = null;
                if (Id == null)
                {
                    return null;
                }

                if (Id.StartsWith("card_"))
                {
                    type = PaymentMethodType.CreditCard;
                }

                else if (Id.StartsWith("bank_"))
                {
                    type = PaymentMethodType.BankAccount;
                }

                return type;
            }
        }

        internal string? Endpoint
        {
            get
            {
                if (Type == null)
                {
                    throw new EasyPostError("Unknown payment method type");
                }

                return Type.EndPoint;
            }
        }
    }
}
