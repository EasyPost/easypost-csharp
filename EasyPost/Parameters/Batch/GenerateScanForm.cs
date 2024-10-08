using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Batch
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/batches#manifesting-scan-form">Parameters</a> for <see cref="EasyPost.Services.BatchService.GenerateScanForm(string, GenerateScanForm, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GenerateScanForm : BaseParameters<Models.API.Batch>
    {
        #region Request Parameters

        /// <summary>
        ///     File format for the scan form.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "file_format")]
        public string? FileFormat { get; set; }

        #endregion
    }
}
