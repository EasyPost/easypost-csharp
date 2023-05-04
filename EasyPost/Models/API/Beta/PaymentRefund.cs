using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API.Beta
{
    public class PaymentRefund : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("refunded_amount")]
        public int? RefundedAmount { get; internal set; }

        // ReSharper disable once StringLiteralTypo
        [JsonProperty("refunded_amount_currencys")]
        public string? RefundedAmountCurrencies { get; internal set; }
        [JsonProperty("refunded_payment_logs")]
        public List<string>? RefundedPaymentLogIds { get; internal set; }
        [JsonProperty("payment_log_id")]
        public string? PaymentLogId { get; internal set; }
        [JsonProperty("errors")]
        public List<Error>? Errors { get; internal set; }

        #endregion

        internal PaymentRefund()
        {
        }
    }
}
