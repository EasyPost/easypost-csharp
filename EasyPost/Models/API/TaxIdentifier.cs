using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TaxIdentifier : EasyPostObject, ITaxIdentifierParameter
    {
        #region JSON Properties

        [JsonProperty("entity")]
        public string? Entity { get; internal set; }
        [JsonProperty("issuing_country")]
        public string? IssuingCountry { get; internal set; }
        [JsonProperty("tax_id")]
        public string? TaxId { get; internal set; }
        [JsonProperty("tax_id_type")]
        public string? TaxIdType { get; internal set; }

        #endregion

        internal TaxIdentifier()
        {
        }
    }
}
