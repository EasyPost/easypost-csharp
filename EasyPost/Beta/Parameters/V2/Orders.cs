using System.Collections.Generic;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Beta.Parameters.V2
{
    public static class Orders
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.OrderService.Create"/> API calls.
        /// </summary>
        public class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "order", "carrier_accounts")]
            public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

            [RequestParameter(Necessity.Optional, "order", "from_address")]
            public EasyPost.Models.API.Address? FromAddress { get; set; }

            [RequestParameter(Necessity.Optional, "order", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "order", "shipments")]
            public List<EasyPost.Models.API.Shipment>? Shipments { get; set; }

            [RequestParameter(Necessity.Optional, "order", "to_address")]
            public EasyPost.Models.API.Address? ToAddress { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.Order"/> one-call buy API calls.
        /// </summary>
        public sealed class OneCallBuy : Create
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "order", "carrier")]
            public string? Carrier { get; set; }

            [RequestParameter(Necessity.Optional, "order", "service")]
            public string? Service { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.Order"/> buy API calls.
        /// </summary>
        public sealed class Buy : RequestParameters
        {
            #region Request Parameters

            #endregion
        }
    }
}
