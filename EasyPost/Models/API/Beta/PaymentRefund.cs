using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API.Beta
{
    /// <summary>
    ///     Class representing an EasyPost payment refund.
    /// </summary>
    public class PaymentRefund : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The amount of the refund, in the associated currency and units (e.g. cents).
        /// </summary>
        [JsonProperty("refunded_amount")]
        public int? RefundedAmount { get; set; }

        /// <summary>
        ///     The currency of the refund. Defaults to USD.
        /// </summary>
        // ReSharper disable once StringLiteralTypo
        [JsonProperty("refunded_amount_currencys")]
        public string? RefundedAmountCurrencies { get; set; }

        /// <summary>
        ///     The IDs of the logs of the payments being refunded.
        /// </summary>
        [JsonProperty("refunded_payment_logs")]
        public List<string>? RefundedPaymentLogIds { get; set; }

        /// <summary>
        ///     The ID of the new payment log created for the refund.
        /// </summary>
        [JsonProperty("payment_log_id")]
        public string? PaymentLogId { get; set; }

        /// <summary>
        ///     A list of <see cref="Error"/>s encountered while processing the refund.
        /// </summary>
        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaymentRefund"/> class.
        /// </summary>
        internal PaymentRefund()
        {
        }
    }
}
