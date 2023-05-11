using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#regenerate-rates-for-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.RegenerateRates(string, RegenerateRates, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RegenerateRates : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     Whether or not to include carbon offsets in the new <see cref="Models.API.Rate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        public bool CarbonOffset { get; set; } = false; // non-nullable, will always be included (default: false)

        #endregion
    }
}
