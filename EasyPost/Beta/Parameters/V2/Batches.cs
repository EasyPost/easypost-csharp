using System.Collections.Generic;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Beta.Parameters.V2
{
    public static class Batches
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.BatchService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "batch", "shipments")]
            public List<EasyPost.Models.API.Shipment>? Shipments { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.Batch"/> shipment update API calls.
        /// </summary>
        public sealed class UpdateShipments : RequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "shipments")]
            public List<EasyPost.Models.API.Shipment>? Shipments { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.Batch"/> document creation API calls.
        /// </summary>
        public sealed class CreateDocument : RequestParameters
        {
            #region Request Parameters

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.BatchService.All"/> list API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
