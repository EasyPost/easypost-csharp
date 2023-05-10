using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#convert-the-label-format-of-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.GenerateLabel(string, GenerateLabel, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class GenerateLabel : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The file format for the new label.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "file_format")]
        public string? FileFormat { get; set; }

        #endregion
    }
}
