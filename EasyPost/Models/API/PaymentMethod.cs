using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Represents a credit card or a bank account.
    /// </summary>
    // Have to collect all possible properties since we can't anticipate which type of payment method the API will return.
    public class PaymentMethod : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the bank associated with the payment method.
        ///     This is only available for bank accounts.
        /// </summary>
        [JsonProperty("bank_name")]
        public string? BankName { get; set; } // bank_account

        /// <summary>
        ///     The brand of the credit card stored in the payment method.
        ///     This is only available for credit cards.
        /// </summary>
        [JsonProperty("brand")]
        public string? Brand { get; set; } // credit_card

        /// <summary>
        ///     The country of the bank associated with the payment method.
        ///     This is only available for bank accounts.
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; } // bank_account

        /// <summary>
        ///     When the payment method was last disabled.
        /// </summary>
        [JsonProperty("disabled_at")]
        public string? DisabledAt { get; set; } // both

        /// <summary>
        ///     The expiration month of the credit card stored in the payment method.
        ///     This is only available for credit cards.
        /// </summary>
        [JsonProperty("exp_month")]
        public int? ExpMonth { get; set; } // credit_card

        /// <summary>
        ///     The expiration year of the credit card stored in the payment method.
        ///     This is only available for credit cards.
        /// </summary>
        [JsonProperty("exp_year")]
        public int? ExpYear { get; set; } // credit_card

        /// <summary>
        ///     The last 4 digits of the credit card or bank account stored in the payment method.
        /// </summary>
        [JsonProperty("last4")]
        public string? Last4 { get; set; } // both

        /// <summary>
        ///     The name on the credit card stored in the payment method.
        ///     This is only available for credit cards.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; } // credit_card

        /// <summary>
        ///     Whether the bank account in the payment method has been verified.
        ///     This is only available for bank accounts.
        /// </summary>
        [JsonProperty("verified")]
        public bool? Verified { get; set; } // bank_account

        /// <summary>
        ///     Whether the payment method requires mandate collection or not.
        /// </summary>
        [JsonProperty("requires_mandate_collection")]
        public bool? RequiresMandateCollection { get; set; } // both

        #endregion

        /// <summary>
        ///     Gets what type of payment method this is (credit card, bank account, etc.)
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

                if (Object == "CreditCard")
                {
                    type = PaymentMethodType.CreditCard;
                }
                else if (Object == "BankAccount")
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
#pragma warning disable CA1507 // IDE doesn't understand what I'm doing here
                    throw new MissingPropertyError(this, nameof(Type));
#pragma warning restore CA1507
                }

                return Type.EndPoint;
            }
        }

        /// <summary>
        ///     Payment method priority.
        /// </summary>
        public class Priority : ValueEnum
        {
            /// <summary>
            ///     An enum representing the primary priority.
            /// </summary>
            public static readonly Priority Primary = new(1, "Primary");

            /// <summary>
            ///     An enum representing the secondary priority.
            /// </summary>
            public static readonly Priority Secondary = new(2, "Secondary");

            private Priority(int id, object value)
                : base(id, value)
            {
            }
        }
    }
}
