using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/shipments#buy-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.Buy(string, Buy, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Buy : BaseParameters<Models.API.Shipment>
    {
        #region Request Parameters

        /// <summary>
        ///     The ID of the <see cref="Rate"/> to purchase a <see cref="Models.API.Shipment"/> with.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "rate", "id")]
        public string? RateId { get; set; }

        /// <summary>
        ///     A value to insure the <see cref="Models.API.Shipment"/> for. Optional.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "insurance")]
        public string? InsuranceValue { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Models.API.EndShipper"/> to buy the <see cref="Models.API.Shipment"/> with. Optional.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "end_shipper")]
        public string? EndShipperId { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Buy"/> class with the given <see cref="Rate"/>.
        /// </summary>
        /// <param name="rate">The selected <see cref="Rate"/>.</param>
        public Buy(Rate rate)
        {
            RateId = rate.Id;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Buy"/> class with the given <see cref="Rate"/> ID.
        /// </summary>
        /// <param name="rateId">The selected <see cref="Rate"/> ID.</param>
        public Buy(string rateId)
        {
            RateId = rateId;
        }
    }
}
