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

        // ID can't be provided when creating a parcel, but can be provided when using a parcel in a non-parcel creation request.
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "height")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "height")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "height")]
        public double? Height { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "length")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "length")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "length")]
        public double? Length { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "weight")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "weight")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "weight")]
        public double? Weight { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "width")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "width")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "width")]
        public double? Width { get; set; }

        #endregion
    }
}
