using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#buy-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.Buy(string, Buy, System.Threading.CancellationToken)"/> API calls.
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
        ///     Whether or not to purchase the <see cref="Models.API.Shipment"/> with a carbon offset. Defaults to <c>false</c>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        public bool? CarbonOffset { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Models.API.EndShipper"/> to buy the <see cref="Models.API.Shipment"/> with. Optional.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "end_shipper")]
        public string? EndShipperId { get; set; }

        #endregion

        /// <summary>
        ///     Construct this parameters set with the given <see cref="Rate"/>.
        /// </summary>
        /// <param name="rate">The selected <see cref="Rate"/>.</param>
        public Buy(Rate rate)
        {
            RateId = rate.Id;
        }

        /// <summary>
        ///     Construct this parameters set with the given <see cref="Rate"/> ID.
        /// </summary>
        /// <param name="rateId">The selected <see cref="Rate"/> ID.</param>
        public Buy(string rateId)
        {
            RateId = rateId;
        }
    }
}
