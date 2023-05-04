using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-batch">Parameters</a> for <see cref="EasyPost.Services.BatchService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IBatchParameter
    {
        #region Request Parameters

        /// <summary>
        ///     List of <see cref="Models.API.Shipment"/>s (or <see cref="Parameters.Shipments.Create"/> parameters) for the new <see cref="Models.API.Batch"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "batch", "shipments")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Required, "shipments")]
        public List<IShipmentParameter>? Shipments { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.Batch"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "batch", "reference")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "reference")]
        public string? Reference { get; set; }

        #endregion
    }
}
