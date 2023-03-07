using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Parcels
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ParcelService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IParcelParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "height")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "height")]
        public double? Height { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "length")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "length")]
        public double? Length { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "weight")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "weight")]
        public double? Weight { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "width")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "width")]
        public double? Width { get; set; }

        #endregion
    }
}
