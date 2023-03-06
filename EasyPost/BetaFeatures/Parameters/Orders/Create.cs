using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Orders
{
    public class Create : BaseParameters, IOrderParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "order", "carrier_accounts")]
        public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "from_address")]
        public EasyPost.Models.API.Address? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "shipments")]
        public List<EasyPost.Models.API.Shipment>? Shipments { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "to_address")]
        public EasyPost.Models.API.Address? ToAddress { get; set; }

        #endregion
    }
}
