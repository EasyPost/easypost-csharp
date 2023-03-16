using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Batch.AddShipments(AddShipments)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AddShipments : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "shipments")]
        public List<Shipment>? Shipments { get; set; } // Shipments have to exist before they can be added to a batch

        #endregion
    }
}
