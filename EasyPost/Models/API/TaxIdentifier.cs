using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TaxIdentifier
    {
        #region JSON Properties

        [JsonProperty("entity")]
        public string entity { get; set; }
        [JsonProperty("issuing_country")]
        public string issuing_country { get; set; }
        [JsonProperty("tax_id")]
        public string tax_id { get; set; }
        [JsonProperty("tax_id_type")]
        public string tax_id_type { get; set; }

        #endregion
    }
}
