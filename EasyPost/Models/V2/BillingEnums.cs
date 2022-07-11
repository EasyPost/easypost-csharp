using System.Collections.Generic;
using EasyPost.Utilities;

namespace EasyPost.Models.V2
{
    /// <summary>
    ///     Enum for the payment method priorities (i.e. primary, secondary, etc.)
    /// </summary>
    public class PaymentMethodPriority : Enum
    {
        public static readonly PaymentMethodPriority Primary = new PaymentMethodPriority(1);
        public static readonly PaymentMethodPriority Secondary = new PaymentMethodPriority(2);

        private PaymentMethodPriority(int id)
            : base(id)
        {
        }

        public static IEnumerable<PaymentMethodPriority> All()
        {
            return GetAll<PaymentMethodPriority>();
        }
    }

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
