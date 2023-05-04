using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Parcel : EasyPostObject, IParcelParameter
    {
        #region JSON Properties

        [JsonProperty("height")]
        public double? Height { get; internal set; }
        [JsonProperty("length")]
        public double? Length { get; internal set; }
        [JsonProperty("predefined_package")]
        public string? PredefinedPackage { get; internal set; }
        [JsonProperty("weight")]
        public double? Weight { get; internal set; }
        [JsonProperty("width")]
        public double? Width { get; internal set; }

        #endregion

        internal Parcel()
        {
        }
    }
}
