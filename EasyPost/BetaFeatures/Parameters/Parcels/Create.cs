using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Parcels
{
    public class Create : BaseParameters, IParcelParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "height")]
        public double? Height { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "length")]
        public double? Length { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "weight")]
        public double? Weight { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "parcel", "width")]
        public double? Width { get; set; }

        #endregion
    }
}
