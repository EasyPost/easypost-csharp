using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API.Beta
{
    public class PaymentRefund : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("refunded_amount")]
        public int? RefundedAmount { get; set; }

        // ReSharper disable once StringLiteralTypo
        [JsonProperty("refunded_amount_currencys")]
        public string? RefundedAmountCurrencies { get; set; }
        [JsonProperty("refunded_payment_logs")]
        public List<string>? RefundedPaymentLogIds { get; set; }
        [JsonProperty("payment_log_id")]
        public string? PaymentLogId { get; set; }
        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }

        #endregion

        internal PaymentRefund()
        {
        }
    }
}
