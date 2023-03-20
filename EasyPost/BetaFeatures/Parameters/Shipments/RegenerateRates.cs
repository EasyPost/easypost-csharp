using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Shipment.RegenerateRates(RegenerateRates)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RegenerateRates : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        public bool CarbonOffset { get; set; } = false; // non-nullable, will always be included (default: false)

        #endregion
    }
}
