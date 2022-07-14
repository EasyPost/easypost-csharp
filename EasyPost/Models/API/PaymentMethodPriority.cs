using System.Collections.Generic;
using EasyPost.Utilities;

namespace EasyPost.Models.API
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
}
