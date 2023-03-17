using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Orders
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Order.Buy(Buy)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Buy : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "carrier")]
        public string? Carrier { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "service")]
        public string? Service { get; set; }

        #endregion

        /// <summary>
        ///     Construct this parameters set with the given carrier and service.
        /// </summary>
        /// <param name="withCarrier">The selected carrier.</param>
        /// <param name="withService">The selected service level.</param>
        public Buy(string withCarrier, string withService)
        {
            Carrier = withCarrier;
            Service = withService;
        }

        /// <summary>
        ///     Construct this parameters set with the given <see cref="Rate"/>.
        /// </summary>
        /// <param name="rate">The selected <see cref="Rate"/>.</param>
        public Buy(Rate rate)
        {
            Carrier = rate.Carrier;
            Service = rate.Service;
        }
    }
}
