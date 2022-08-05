using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Represents a summary of the primary and secondary payment methods on the user's account.
    /// </summary>
    public class PaymentMethodsSummary : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("primary_payment_method")]
        public PaymentMethod primary_payment_method { get; set; }
        [JsonProperty("secondary_payment_method")]
        public PaymentMethod secondary_payment_method { get; set; }

        #endregion
    }
}
