using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Orders
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.OrderService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IOrderParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "order", "carrier_accounts")]
        public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "shipments")]
        public List<IShipmentParameter>? Shipments { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "order", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        #endregion
    }
}
