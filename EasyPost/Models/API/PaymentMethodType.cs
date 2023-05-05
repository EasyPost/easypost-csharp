using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Enum representing an EasyPost payment method type.
    /// </summary>
    public class PaymentMethodType : ValueEnum
    {
        /// <summary>
        ///     The bank account payment method type.
        /// </summary>
        public static readonly PaymentMethodType BankAccount = new(2, "bank_accounts");

        /// <summary>
        ///     The credit card payment method type.
        /// </summary>
        public static readonly PaymentMethodType CreditCard = new(1, "credit_cards");

        /// <summary>
        ///     Gets the endpoint for this payment method type.
        /// </summary>
        internal string? EndPoint => Value.ToString();

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaymentMethodType"/> class.
        /// </summary>
        /// <param name="id">The internal ID of this enum.</param>
        /// <param name="endpoint">The endpoint associated with this enum.</param>
        private PaymentMethodType(int id, string endpoint)
            : base(id, endpoint)
        {
        }
    }
}
