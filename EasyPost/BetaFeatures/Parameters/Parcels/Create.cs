using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Parcels
{
    public class Create : BaseParameters, IParcelParameter
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
