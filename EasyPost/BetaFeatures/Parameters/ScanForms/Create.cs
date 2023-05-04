using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.ScanForms
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-scanform">Parameters</a> for <see cref="EasyPost.Services.ScanFormService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IScanFormParameter
    {
        #region Request Parameters

        /// <summary>
        ///     A list of <see cref="Models.API.Shipment"/>s (or <see cref="Parameters.Shipments.Create"/> parameter sets) to use to create a <see cref="Models.API.ScanForm"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "shipments")]
        public List<IShipmentParameter>? Shipments { get; set; }

        #endregion
    }
}
