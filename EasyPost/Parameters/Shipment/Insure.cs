using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#insure-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.Insure(string, Insure, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Insure : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The amount to insure the <see cref="Models.API.Shipment"/> for.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "amount")]
        public string? Amount { get; set; }

        #endregion
    }
}
