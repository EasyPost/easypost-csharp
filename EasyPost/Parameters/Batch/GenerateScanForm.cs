using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Batch
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#manifesting-scan-form">Parameters</a> for <see cref="EasyPost.Services.BatchService.GenerateScanForm(string, GenerateScanForm, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GenerateScanForm : BaseParameters
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
