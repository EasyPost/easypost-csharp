using EasyPost._base;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#parcel-object">EasyPost parcel</a>.
    /// </summary>
    public class Parcel : EasyPostObject, IParcelParameter
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parcel"/> class.
        /// </summary>
        internal Parcel()
        {
        }
    }
}
