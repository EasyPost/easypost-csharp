using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    public class Create : BaseParameters, IBatchParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "batch", "shipments")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Required, "shipments")]
        public List<IShipmentParameter>? Shipments { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "batch", "reference")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Required, "reference")]
        public string? Reference { get; set; }

        #endregion
    }
}
