using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Insurance
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/insurance#create-an-insurance">Parameters</a> for <see cref="EasyPost.Services.InsuranceService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Insurance>, IInsuranceParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Value of the content to insure, in USD. Maximum $5,000.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance", "amount")]
        public double? Amount { get; set; }

        /// <summary>
        ///     Carrier associated with the <see cref="TrackingCode"/>.
        ///     Auto-derived from <see cref="TrackingCode"/> if not provided.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance", "carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The origin <see cref="Models.API.Address"/> (or <see cref="Address.Create"/> parameters) for the shipment.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.Insurance"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The destination <see cref="Models.API.Address"/> (or <see cref="Address.Create"/> parameters) for the shipment.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     The tracking code associated with the non-EasyPost-purchased package to insure.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance", "tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion
    }
}
