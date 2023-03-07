using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Pickups
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.PickupService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IPickupParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "address")]
        public IAddressParameter? Address { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "batch")]
        public IBatchParameter? Batch { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "carrier_accounts")]
        public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "instructions")]
        public string? Instructions { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "is_account_address")]
        public bool? IsAccountAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "max_datetime")]
        public DateTime? MaxDatetime { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "min_datetime")]
        public DateTime? MinDatetime { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "pickup", "shipment")]
        public IShipmentParameter? Shipment { get; set; }

        #endregion
    }
}
