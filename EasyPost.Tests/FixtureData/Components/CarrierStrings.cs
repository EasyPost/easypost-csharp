using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class CarrierStrings
    {
        #region JSON Properties

        [JsonProperty("usps")]
        public string Usps { get; set; }

        #endregion
    }
}
