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

        [JsonProperty("height")]
        public double? Height { get; set; }
        [JsonProperty("length")]
        public double? Length { get; set; }
        [JsonProperty("predefined_package")]
        public string? PredefinedPackage { get; set; }
        [JsonProperty("weight")]
        public double? Weight { get; set; }
        [JsonProperty("width")]
        public double? Width { get; set; }

        #endregion

    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.Parcel
