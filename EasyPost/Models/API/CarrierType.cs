using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#carriertype-object">EasyPost carrier type</a>.
    /// </summary>
    public class CarrierType : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     Expanded credential fields.
        ///     Contains at least one of the following keys:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"auto_link"</description>
        ///         </item>
        ///         <item>
        ///             <description>"credentials"</description>
        ///         </item>
        ///         <item>
        ///             <description>"test_credentials"</description>
        ///         </item>
        ///         <item>
        ///             <description>"custom_workflow"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("fields")]
        public Dictionary<string, object>? Fields { get; set; } // TODO: This should be a CarrierFields object.

        /// <summary>
        ///     The logo for the carrier.
        /// </summary>
        [JsonProperty("logo")]
        public string? Logo { get; set; }

        /// <summary>
        ///     The human-readable name of the carrier.
        /// </summary>
        [JsonProperty("readable")]
        public string? Readable { get; set; }

        /// <summary>
        ///     The type of the carrier.
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

    }
}
