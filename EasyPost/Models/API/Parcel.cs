using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Parcel
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#parcel-object">EasyPost parcel</a>.
    /// </summary>
    public class Parcel : EasyPostObject, Parameters.IParcelParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The height of the parcel, in inches.
        /// </summary>
        [JsonProperty("height")]
        public double? Height { get; set; }

        /// <summary>
        ///     The length of the parcel, in inches.
        /// </summary>
        [JsonProperty("length")]
        public double? Length { get; set; }

        /// <summary>
        ///     A predefined package type to use for the parcel, instead of specifying dimensions.
        /// </summary>
        [JsonProperty("predefined_package")]
        public string? PredefinedPackage { get; set; }

        /// <summary>
        ///     The weight of the parcel, in ounces.
        /// </summary>
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        /// <summary>
        ///     The width of the parcel, in inches.
        /// </summary>
        [JsonProperty("width")]
        public double? Width { get; set; }

        #endregion

    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.Parcel
