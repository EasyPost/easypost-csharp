using System;
using System.Collections.Generic;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class Pickups
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.PickupService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "pickup", "address")]
            public EasyPost.Models.API.Address? Address { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "batch")]
            public EasyPost.Models.API.Batch? Batch { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "carrier_accounts")]
            public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "instructions")]
            public string? Instructions { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "is_account_address")]
            public bool IsAccountAddress { get; set; } = false;

            [RequestParameter(Necessity.Optional, "pickup", "max_datetime")]
            public DateTime? MaxDatetime { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "min_datetime")]
            public DateTime? MinDatetime { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "pickup", "shipment")]
            public EasyPost.Models.API.Shipment? Shipment { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.Pickup"/> buy API calls.
        /// </summary>
        public sealed class Buy : RequestParameters
        {
            #region Request Parameters

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.PickupService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
