using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ShipmentService.RetrieveEstimatedDeliveryDate(string, RetrieveEstimatedDeliveryDate)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RetrieveEstimatedDeliveryDate : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "planned_ship_date")]
        public string? PlannedShipDate { get; set; }

        #endregion
    }
}
