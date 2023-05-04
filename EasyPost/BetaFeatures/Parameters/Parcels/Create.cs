using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Parcels
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-parcel">Parameters</a> for <see cref="EasyPost.Services.ParcelService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IParcelParameter
    {
        #region Request Parameters

        /// <summary>
        ///     ID of the <see cref="Models.API.Parcel"/> to reference in this request.
        ///     ID is not used when calling <see cref="Services.ParcelService.Create(Create, System.Threading.CancellationToken)"/>,
        ///     but is used when using this parameter set as a nested parameter set in other API calls.
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Height of the <see cref="Models.API.Parcel"/>, in inches.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "height")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "height")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "height")]
        public double? Height { get; set; }

        /// <summary>
        ///     Length of the <see cref="Models.API.Parcel"/>, in inches.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "length")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "length")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "length")]
        public double? Length { get; set; }

        /// <summary>
        ///     Weight of the <see cref="Models.API.Parcel"/>, in ounces.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "weight")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "weight")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "weight")]
        public double? Weight { get; set; }

        /// <summary>
        ///     Width of the <see cref="Models.API.Parcel"/>, in inches.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "width")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "width")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "width")]
        public double? Width { get; set; }

        #endregion
    }
}
