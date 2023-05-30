using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.TaxIdentifier
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#taxidentifier-object">EasyPost tax identifier</a>.
    /// </summary>
    public class TaxIdentifier : EasyPostObject, Parameters.ITaxIdentifierParameter
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

        
    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.TaxIdentifier
