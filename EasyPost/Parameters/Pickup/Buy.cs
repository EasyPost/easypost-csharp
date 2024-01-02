using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Pickup
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#buy-a-pickup">Parameters</a> for <see cref="EasyPost.Services.PickupService.Buy(string, Buy, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Buy : BaseParameters<Models.API.Pickup>
    {
        #region Request Parameters

        /// <summary>
        ///     Carrier for use to buy the <see cref="Models.API.Pickup"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     Service level to use to buy the <see cref="Models.API.Pickup"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "service")]
        public string? Service { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Buy"/> class with the given carrier and service.
        /// </summary>
        /// <param name="withCarrier">The selected carrier.</param>
        /// <param name="withService">The selected service level.</param>
        public Buy(string withCarrier, string withService)
        {
            Carrier = withCarrier;
            Service = withService;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Buy"/> class with the given <see cref="Rate"/>.
        /// </summary>
        /// <param name="rate">The selected <see cref="Rate"/>.</param>
        public Buy(Rate rate)
        {
            Carrier = rate.Carrier;
            Service = rate.Service;
        }
    }
}
