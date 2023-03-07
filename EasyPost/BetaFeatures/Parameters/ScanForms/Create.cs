using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.ScanForms
{
    public class Create : BaseParameters, IScanFormParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "shipments")]
        public List<IShipmentParameter>? Shipments { get; set; }

        #endregion
    }
}
