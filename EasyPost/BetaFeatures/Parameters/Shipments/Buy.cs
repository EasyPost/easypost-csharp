using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Shipment.Buy(Buy)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Buy : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "rate", "id")]
        public string? RateId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "insurance")]
        public string? InsuranceValue { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        public bool? CarbonOffset { get; set; }

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
    }
}
