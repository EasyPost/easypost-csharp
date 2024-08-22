using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Batch
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/batches#batch-labels">Parameters</a> for <see cref="EasyPost.Services.BatchService.GenerateLabel(string, GenerateLabel, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GenerateLabel : BaseParameters<Models.API.Batch>
    {
        #region Request Parameters

        /// <summary>
        ///     File format for the label.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "file_format")]
        public string? FileFormat { get; set; }

        #endregion
    }
}
