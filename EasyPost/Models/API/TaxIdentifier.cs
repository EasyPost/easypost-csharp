using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TaxIdentifier
    {
        #region JSON Properties

        [JsonProperty("entity")]
        public string? Entity { get; set; }
        [JsonProperty("issuing_country")]
        public string? IssuingCountry { get; set; }
        [JsonProperty("tax_id")]
        public string? TaxId { get; set; }
        [JsonProperty("tax_id_type")]
        public string? TaxIdType { get; set; }

        #endregion

        internal TaxIdentifier()
        {
        }
    }
}
