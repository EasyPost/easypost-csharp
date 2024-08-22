using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Parcel
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/parcels#create-a-parcel">Parameters</a> for <see cref="EasyPost.Services.ParcelService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Parcel>, IParcelParameter
    {
        #region Request Parameters

        /// <summary>
        ///     ID of the <see cref="Models.API.Parcel"/> to reference in this request.
        ///     ID is not used when calling <see cref="Services.ParcelService.Create(Create, System.Threading.CancellationToken)"/>,
        ///     but is used when using this parameter set as a nested parameter set in other API calls.
        /// </summary>
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Height of the <see cref="Models.API.Parcel"/>, in inches.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "height")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "height")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "height")]
        public double? Height { get; set; }

        /// <summary>
        ///     Length of the <see cref="Models.API.Parcel"/>, in inches.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "length")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "length")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "length")]
        public double? Length { get; set; }

        /// <summary>
        ///     Weight of the <see cref="Models.API.Parcel"/>, in ounces.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "weight")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "weight")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "weight")]
        public double? Weight { get; set; }

        /// <summary>
        ///     Width of the <see cref="Models.API.Parcel"/>, in inches.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "width")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "width")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "width")]
        public double? Width { get; set; }

        /// <summary>
        ///     Predefined package type to use for the <see cref="Models.API.Parcel"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "parcel", "predefined_package")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "predefined_package")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "predefined_package")]
        public string? PredefinedPackage { get; set; }

        #endregion
    }
}
