using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Claim
    /// <summary>
    ///     Class representing a EasyPost claim object.
    /// </summary>
    public class Claim : EasyPostObject, Parameters.IClaimParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The amount that has been approved for reimbursement.
        /// </summary>
        [JsonProperty("approved_amount")]
        public string? ApprovedAmount { get; set; }

        /// <summary>
        ///    The attachments associated with the claim.
        /// </summary>
        [JsonProperty("attachments")]
        public List<string>? Attachments { get; set; }

        /// <summary>
        ///     The address to which the reimbursement check should be sent.
        /// </summary>
        [JsonProperty("check_delivery_address")]
        public string? CheckDeliveryAddress { get; set; }

        /// <summary>
        ///     The email address of the contact person for the claim.
        /// </summary>
        [JsonProperty("contact_email")]
        public string? ContactEmail { get; set; }

        /// <summary>
        ///     A detailed description of the claim.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     A list of <see cref="ClaimHistoryEntry"/>s representing the history of the claim.
        /// </summary>
        [JsonProperty("history")]
        public List<ClaimHistoryEntry>? History { get; set; }

        /// <summary>
        ///     The original amount of the insurance on the associated <see cref="Models.API.Shipment"/>.
        /// </summary>
        [JsonProperty("insurance_amount")]
        public double? InsuranceAmount { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Models.API.Insurance"/> object associated with this claim.
        /// </summary>
        [JsonProperty("insurance_id")]
        public string? InsuranceId { get; set; }

        // ReSharper disable once InconsistentNaming
#pragma warning disable SA1300
        [JsonProperty("payment_method")]
        private string? _paymentMethod { get; set; }
#pragma warning restore SA1300

        /// <summary>
        ///     The <see cref="ClaimPaymentMethod"/> of the claim.
        /// </summary>
#pragma warning disable SA1101
        [JsonIgnore]
        public ClaimPaymentMethod? PaymentMethod => _paymentMethod == null ? null : ValueEnum.FromValue<ClaimPaymentMethod>(_paymentMethod);
#pragma warning disable SA1101

        /// <summary>
        ///     The name of the recipient of the reimbursement.
        /// </summary>
        [JsonProperty("recipient_name")]
        public string? RecipientName { get; set; }

        /// <summary>
        ///     An optional value that may be used in place of ID when doing Retrieve calls for this claim.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The amount that has been requested for reimbursement.
        /// </summary>
        [JsonProperty("requested_amount")]
        public double? RequestedAmount { get; set; }

        /// <summary>
        ///     The salvage value of the damaged goods.
        /// </summary>
        [JsonProperty("salvage_value")]
        public double? SalvageValue { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Models.API.Shipment"/> associated with this claim.
        /// </summary>
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }

        /// <summary>
        ///     The current status of the claim.
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     The reason for the current status of the claim.
        /// </summary>
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; set; }

        /// <summary>
        ///     The timestamp of the last status update.
        /// </summary>
        [JsonProperty("status_timestamp")]
        public string? StatusTimestamp { get; set; }

        /// <summary>
        ///     The tracking code of the <see cref="Models.API.Shipment"/> associated with this claim.
        /// </summary>
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        [JsonProperty("type")]
        // ReSharper disable once InconsistentNaming
#pragma warning disable SA1300
        private string? _type { get; set; }
#pragma warning restore SA1300

        /// <summary>
        ///     The <see cref="ClaimType"/> of the claim.
        /// </summary>
        [JsonIgnore]
#pragma warning disable SA1101
        public ClaimType? Type => _type == null ? null : ValueEnum.FromValue<ClaimType>(_type);
#pragma warning restore SA1101

        #endregion
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.Claim

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Claim"/> objects.
    /// </summary>
    public class ClaimCollection : PaginatedCollection<Claim>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Claim"/>s in the collection.
        /// </summary>
        [JsonProperty("claims")]
        public List<Claim>? Claims { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Claim> entries, int? pageSize = null)
        {
            Parameters.Claim.All parameters = Filters != null ? (Parameters.Claim.All)Filters : new Parameters.Claim.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
