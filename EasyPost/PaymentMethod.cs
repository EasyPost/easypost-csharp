using System.Threading.Tasks;
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
    /// </summary>
    public class PaymentMethodObject : Resource
    {
        public enum PaymentMethodType
        {
            CreditCard,
            BankAccount
        }

        [JsonProperty("bank_name")]
        public string bank_name { get; set; }
        [JsonProperty("brand")]
        public string brand { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("disabled_at")]
        public string disabled_at { get; set; }
        [JsonProperty("exp_month")]
        public string exp_month { get; set; }
        [JsonProperty("exp_year")]
        public string exp_year { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("last4")]
        public string last4 { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }

        /// <summary>
        ///     Get what type of payment method this is (credit card, bank account, etc.)
        /// </summary>
        public PaymentMethodType? Type
        {
            get
            {
                if (id == null)
                {
                    return null;
                }

                if (id.StartsWith("card_"))
                {
                    return PaymentMethodType.CreditCard;
                }

                if (id.StartsWith("bank_"))
                {
                    return PaymentMethodType.BankAccount;
                }

                return null;
            }
        }

        [JsonProperty("verified")]
        public bool verified { get; set; }

        /// <summary>
        ///     Delete this payment method from the user's account.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public async Task<bool> Delete()
        {
            return await Billing.DeletePaymentMethod(id);
        }
    }
}
