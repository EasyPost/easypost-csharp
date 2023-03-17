using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Shipment.GenerateLabel(GenerateLabel)"/> API calls.
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
