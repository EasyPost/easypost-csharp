using Newtonsoft.Json;

namespace EasyPost
{
    public class Brand
    {
        [JsonProperty("ad")]
        public string ad { get; set; }
        [JsonProperty("ad_href")]
        public string ad_href { get; set; }
        [JsonProperty("background_color")]
        public string background_color { get; set; }
        [JsonProperty("color")]
        public string color { get; set; }
        [JsonProperty("logo")]
        public string logo { get; set; }
        [JsonProperty("logo_href")]
        public string logo_href { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("theme")]
        public string theme { get; set; }
        [JsonProperty("user_id")]
        public string user_id { get; set; }
    }
}
