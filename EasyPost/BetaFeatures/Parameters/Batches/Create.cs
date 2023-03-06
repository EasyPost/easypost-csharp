using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    public class Create : BaseParameters, IBatchParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "batch", "shipments")]
        public List<EasyPost.Models.API.Shipment>? Shipments { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "batch", "reference")]
        public string? Reference { get; set; }

        #endregion
    }
}
