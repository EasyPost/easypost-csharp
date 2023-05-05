using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Represents a <a href="https://www.easypost.com/docs/api#retrieve-payment-methods">summary of the primary and secondary payment methods on the user's account</a>.
    /// </summary>
    public class PaymentMethodsSummary : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("primary_payment_method")]
        public PaymentMethod? PrimaryPaymentMethod { get; set; }
        [JsonProperty("secondary_payment_method")]
        public PaymentMethod? SecondaryPaymentMethod { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaymentMethodsSummary"/> class.
        /// </summary>
        internal PaymentMethodsSummary()
        {
        }
    }
}
