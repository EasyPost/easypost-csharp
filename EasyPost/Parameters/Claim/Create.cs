using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Claim
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/insurance#create-an-insurance">Parameters</a> for <see cref="EasyPost.Services.ClaimService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Claim>, IClaimParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The tracking code of the <see cref="Models.API.Shipment"/> to file a claim for.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "tracking_code")]
        public string? TrackingCode { get; set; }

        /// <summary>
        ///     The type of claim to file.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "type")]
        public Models.API.ClaimType? Type { get; set; }

        /// <summary>
        ///     The amount being claimed for reimbursement.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "amount")]
        public double? Amount { get; set; }

        /// <summary>
        ///     Email-based files to attach to the claim for evidence.
        ///     Each file must be a base64-encoded string.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "email_evidence_attachments")]
        public string[]? EmailEvidenceAttachments { get; set; }

        /// <summary>
        ///     Invoices to attach to the claim for evidence.
        ///     Each file must be a base64-encoded string.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "invoice_attachments")]
        public string[]? InvoiceAttachments { get; set; }

        /// <summary>
        ///     Additional supporting documents to attach to the claim.
        ///     Required if the <see cref="Type"/> is <see cref="Models.API.ClaimType.Damage"/> or <see cref="Models.API.ClaimType.Theft"/>.
        ///     Each file must be a base64-encoded string.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "supporting_documentation_attachments")]
        public string[]? SupportingDocumentationAttachments { get; set; }

        /// <summary>
        ///     Detailed description of the claim.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The name of the recipient of the reimbursement.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "recipient_name")]
        public string? RecipientName { get; set; }

        /// <summary>
        ///     An optional value that may be used in place of ID when doing Retrieve calls for this claim.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The email address of the contact for the claim.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "contact_email")]
        public string? ContactEmail { get; set; }

        /// <summary>
        ///     The <see cref="ClaimPaymentMethod"/> for the claim reimbursement. If set to <see cref="ClaimPaymentMethod.MailedCheck"/>, the <see cref="CheckDeliveryAddress"/> must be provided.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "payment_method")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfValue, "mailed_check", DependentStatus.MustBeSet, "CheckDeliveryAddress")]
        public ClaimPaymentMethod? PaymentMethod { get; set; }

        /// <summary>
        ///     The destination address for a reimbursement check. Required if the <see cref="PaymentMethod"/> is <see cref="ClaimPaymentMethod.MailedCheck"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "check_delivery_address")]
        public string? CheckDeliveryAddress { get; set; }

        #endregion
    }
}
