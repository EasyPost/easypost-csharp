using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Order
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-an-order">Parameters</a> for <see cref="EasyPost.Services.OrderService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Order>, IOrderParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The <see cref="Address"/> (or <see cref="Address.Create"/> parameters) of the buyer for the new <see cref="Models.API.Order"/>.
        ///     Defaults to <see cref="ToAddress"/> if not provided.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "buyer_address")]
        public IAddressParameter? BuyerAddress { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.CarrierAccount"/>s to use to create the new <see cref="Models.API.Order"/>.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "carrier_accounts")]
        public List<Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        /// <summary>
        ///     <see cref="Models.API.CustomsInfo"/> (or a <see cref="Parameters.CustomsInfo.Create"/> parameter set) for the new <see cref="Models.API.Order"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "customs_info")]
        public ICustomsInfoParameter? CustomsInfo { get; set; }

        /// <summary>
        ///     The origin <see cref="Models.API.Address"/> (or <see cref="Address.Create"/> parameters) for the new <see cref="Models.API.Order"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     Whether the new <see cref="Models.API.Order"/> is a return.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "is_return")]
        public bool? IsReturn { get; set; }

        /// <summary>
        ///     Additional <see cref="Models.API.Options"/> for the new <see cref="Models.API.Order"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "options")]
        public Models.API.Options? Options { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.Order"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The return <see cref="Models.API.Address"/> (or <see cref="Address.Create"/> parameters) for the new <see cref="Models.API.Order"/>.
        ///     Defaults to <see cref="FromAddress"/> if not provided.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "return_address")]
        public IAddressParameter? ReturnAddress { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.Shipment"/>s (or <see cref="Shipment.Create"/> parameter sets) for the new <see cref="Models.API.Order"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "shipments")]
        public List<IShipmentParameter>? Shipments { get; set; }

        /// <summary>
        ///     The destination <see cref="Models.API.Address"/> (or <see cref="Address.Create"/> parameters) for the new <see cref="Models.API.Order"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     One-call-buy an <see cref="Models.API.Order"/> by specifying a service.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "order", "service")]
        public string? Service { get; set; }

        #endregion
    }
}
