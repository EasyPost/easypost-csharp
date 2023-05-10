using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.CustomInfo
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#customs-info-object">EasyPost customs info object</a>.
    /// </summary>
    public class CustomsInfo : EasyPostObject, ICustomsInfoParameter
    {
        #region JSON Properties

        /// <summary>
        ///     A human-readable description of the contents of the package.
        ///     Required for certain carriers, and always required if <see cref="ContentsType"/> is "other".
        ///     Maximum length is 255 characters.
        /// </summary>
        [JsonProperty("contents_explanation")]
        public string? ContentsExplanation { get; set; }

        /// <summary>
        ///     The type of contents in the package.
        ///     May be one of the following:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"documents"</description>
        ///         </item>
        ///         <item>
        ///             <description>"gift"</description>
        ///         </item>
        ///         <item>
        ///             <description>"merchandise"</description>
        ///         </item>
        ///         <item>
        ///             <description>"returned_goods"</description>
        ///         </item>
        ///         <item>
        ///             <description>"sample"</description>
        ///         </item>
        ///         <item>
        ///             <description>"dangerous_goods"</description>
        ///         </item>
        ///         <item>
        ///             <description>"humanitarian_donation"</description>
        ///         </item>
        ///         <item>
        ///             <description>"other"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("contents_type")]
        public string? ContentsType { get; set; }

        /// <summary>
        ///     Whether to electronically certify the information provided.
        /// </summary>
        [JsonProperty("customs_certify")]
        public bool? CustomsCertify { get; set; }

        /// <summary>
        ///     A list of the items being shipped.
        /// </summary>
        [JsonProperty("customs_items")]
        public List<CustomsItem>? CustomsItems { get; set; }

        /// <summary>
        ///     The name of the person signing the customs form.
        ///     Required if <see cref="CustomsCertify"/> is true.
        /// </summary>
        [JsonProperty("customs_signer")]
        public string? CustomsSigner { get; set; }

        /// <summary>
        ///   A customs declaration message, available for eligible carriers.
        /// </summary>
        [JsonProperty("declaration")]
        public string? Declaration { get; set; }

        /// <summary>
        ///     Whether the customs form is marked as "EEL" or "PFC".
        ///     Valid values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"EEL"</description>
        ///         </item>
        ///         <item>
        ///             <description>"PFC"</description>
        ///         </item>
        ///     </list>
        ///     See https://www.easypost.com/customs-guide for more information.
        /// </summary>
        [JsonProperty("eel_pfc")]
        public string? EelPfc { get; set; }

        /// <summary>
        ///     What to do if the package is undeliverable.
        ///     Valid values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"abandon"</description>
        ///         </item>
        ///         <item>
        ///             <description>"return"</description>
        ///         </item>
        ///     </list>
        ///     Default is "return".
        /// </summary>
        [JsonProperty("non_delivery_option")]
        public string? NonDeliveryOption { get; set; }

        /// <summary>
        ///     Explanation for the restriction type.
        ///     Required if <see cref="RestrictionType"/> is not "none".
        /// </summary>
        [JsonProperty("restriction_comments")]
        public string? RestrictionComments { get; set; }

        /// <summary>
        ///     The type of restriction placed on the package.
        ///     Valid values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"none"</description>
        ///         </item>
        ///         <item>
        ///             <description>"quarantine"</description>
        ///         </item>
        ///         <item>
        ///             <description>"sanitary_phytosanitary_inspection"</description>
        ///         </item>
        ///         <item>
        ///             <description>"other"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("restriction_type")]
        public string? RestrictionType { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomsInfo"/> class.
        /// </summary>
        internal CustomsInfo()
        {
        }
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.CustomInfo
}
