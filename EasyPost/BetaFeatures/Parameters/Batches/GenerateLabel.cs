using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#batch-labels">Parameters</a> for <see cref="EasyPost.Services.BatchService.GenerateLabel(string, GenerateLabel, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class GenerateLabel : BaseParameters
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
