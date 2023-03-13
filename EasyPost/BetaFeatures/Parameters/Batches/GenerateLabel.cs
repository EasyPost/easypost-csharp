using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Batches
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Batch.GenerateLabel(GenerateLabel)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class GenerateLabel : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "file_format")]
        public string? FileFormat { get; set; }

        #endregion
    }
}
