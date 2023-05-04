using System;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal;
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

        [JsonProperty("bank_name")]
        public string? BankName { get; internal set; } // bank_account
        [JsonProperty("brand")]
        public string? Brand { get; internal set; } // credit_card
        [JsonProperty("country")]
        public string? Country { get; internal set; } // bank_account
        [JsonProperty("disabled_at")]
        public string? DisabledAt { get; internal set; } // both
        [JsonProperty("exp_month")]
        public int? ExpMonth { get; internal set; } // credit_card
        [JsonProperty("exp_year")]
        public int? ExpYear { get; internal set; } // credit_card
        [JsonProperty("last4")]
        public string? Last4 { get; internal set; } // both
        [JsonProperty("name")]
        public string? Name { get; internal set; } // credit_card
        [JsonProperty("verified")]
        public bool? Verified { get; internal set; } // bank_account

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

                if (Id.StartsWith("card_", StringComparison.InvariantCulture))
                {
                    type = PaymentMethodType.CreditCard;
                }
                else if (Id.StartsWith("bank_", StringComparison.InvariantCulture))
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

        internal PaymentMethod()
        {
        }

        /// <summary>
        ///     Payment method priority.
        /// </summary>
        public class Priority : ValueEnum
        {
            public static readonly Priority Primary = new(1, "Primary");
            public static readonly Priority Secondary = new(2, "Secondary");

            private Priority(int id, object value)
                : base(id, value)
            {
            }
        }
    }
}
