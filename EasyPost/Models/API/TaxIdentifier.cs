using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.TaxIdentifier
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/shipments/tax-identifiers#taxidentifier-object">EasyPost tax identifier</a>.
    /// </summary>
    public class TaxIdentifier : EasyPostObject, Parameters.ITaxIdentifierParameter
    {
        #region JSON Properties

        /// <summary>
        ///     Which entity the <see cref="TaxId"/> belongs to.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"SENDER"</description>
        ///         </item>
        ///         <item>
        ///             <description>"RECEIVER"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("entity")]
        public string? Entity { get; set; }

        /// <summary>
        ///     The issuing country of the <see cref="TaxId"/>.
        /// </summary>
        [JsonProperty("issuing_country")]
        public string? IssuingCountry { get; set; }

        /// <summary>
        ///     The Tax ID number.
        /// </summary>
        [JsonProperty("tax_id")]
        public string? TaxId { get; set; }

        /// <summary>
        ///     The type of <see cref="TaxId"/> provided.
        ///     Possible values are listed <a href="https://support.easypost.com/hc/en-us/articles/4412101923213">on EasyPost's website</a>.
        /// </summary>
        [JsonProperty("tax_id_type")]
        public string? TaxIdType { get; set; }

        #endregion

    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.TaxIdentifier
