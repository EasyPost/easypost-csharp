using EasyPost.Utilities;

namespace EasyPost.Models.API
{
    public class PaymentMethodType : ValueEnum
    {
        public static readonly PaymentMethodType BankAccount = new(2, "bank_accounts");
        public static readonly PaymentMethodType CreditCard = new(1, "credit_cards");

        internal string? EndPoint => Value.ToString();

        private PaymentMethodType(int id, string endpoint) : base(id, endpoint)
        {
        }
    }
}
