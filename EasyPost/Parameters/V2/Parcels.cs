using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class Parcels
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.ParcelService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "parcel", "height")]
            public double? Height { get; set; }

            [RequestParameter(Necessity.Optional, "parcel", "length")]
            public double? Length { get; set; }

            [RequestParameter(Necessity.Optional, "parcel", "weight")]
            public double? Weight { get; set; }

            [RequestParameter(Necessity.Optional, "parcel", "width")]
            public double? Width { get; set; }

            #endregion
        }
    }
}
