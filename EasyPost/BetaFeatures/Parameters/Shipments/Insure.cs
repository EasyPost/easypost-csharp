using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Shipment.Insure(Insure)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Insure : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "amount")]
        public string? Amount { get; set; }

        #endregion
    }
}
