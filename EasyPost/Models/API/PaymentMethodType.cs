using EasyPost.Utilities;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Enum for the payment method types (i.e. credit card, bank account, etc.)
    /// </summary>
    internal class PaymentMethodType : ValueEnum
    {
        public static readonly PaymentMethodType BankAccount = new PaymentMethodType(2, "bank_accounts");
        public static readonly PaymentMethodType CreditCard = new PaymentMethodType(1, "credit_cards");

        internal string EndPoint => Value.ToString()!;

        private PaymentMethodType(int id, string value) : base(id, value)
        {
        }
    }
}
