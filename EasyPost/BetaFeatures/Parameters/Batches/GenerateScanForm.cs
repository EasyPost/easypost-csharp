using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Batch.GenerateScanForm(GenerateScanForm)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class GenerateScanForm : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "file_format")]
        public string? FileFormat { get; set; }

        #endregion
    }
}
