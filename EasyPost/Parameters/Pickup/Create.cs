using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Pickup
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-pickup">Parameters</a> for <see cref="EasyPost.Services.PickupService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Pickup>, IPickupParameter
    {
        #region Request Parameters

        /// <summary>
        ///     <see cref="Models.API.Address"/> (or <see cref="Address.Create"/> parameter set) to pick up package from.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "address")]
        public IAddressParameter? Address { get; set; }

        /// <summary>
        ///     <see cref="Models.API.Batch"/> being set for pickup (required if <see cref="Shipment"/> not provided).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "batch")]
        public IBatchParameter? Batch { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.CarrierAccount"/>s to use to create a new <see cref="Models.API.Pickup"/>.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "carrier_accounts")]
        public List<Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        /// <summary>
        ///     Instructions for the new <see cref="Models.API.Pickup"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "instructions")]
        public string? Instructions { get; set; }

        /// <summary>
        ///    Indicates whether the pickup address is an account address.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "is_account_address")]
        public bool? IsAccountAddress { get; set; }

        /// <summary>
        ///     Maximum pickup date.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "max_datetime")]
        public string? MaxDatetime { get; set; }

        /// <summary>
        ///     Minimum pickup date.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "min_datetime")]
        public string? MinDatetime { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.Pickup"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     <see cref="Models.API.Shipment"/> being set for pickup (required if <see cref="Batch"/> not provided).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pickup", "shipment")]
        public IShipmentParameter? Shipment { get; set; }

        #endregion
    }
}
