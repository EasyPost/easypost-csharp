using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class PageSizes
    {
        #region JSON Properties

        [JsonProperty("fifty_results")]
        public int Fifty { get; set; }

        [JsonProperty("five_results")]
        public int Five { get; set; }
        [JsonProperty("one_result")]
        public int One { get; set; }

        [JsonProperty("one_hundred_results")]
        public int OneHundred { get; set; }

        [JsonProperty("ten_results")]
        public int Ten { get; set; }

        [JsonProperty("twenty_results")]
        public int Twenty { get; set; }

        #endregion
    }
}
