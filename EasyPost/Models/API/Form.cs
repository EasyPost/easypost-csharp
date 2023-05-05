using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#form-object">EasyPost form</a>.
    /// </summary>
    public class Form : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The type of form.
        ///     Possible values include "cn22",  "cod_return_label", "commercial_invoice", "high_value_report", "label_qr_code", "nafta_certificate_of_origin", "order_summary", "return_packing_slip" and "rma_qr_code".
        /// </summary>
        [JsonProperty("form_type")]
        public string? FormType { get; set; }

        /// <summary>
        ///     The URL where the form can be downloaded.
        /// </summary>
        [JsonProperty("form_url")]
        public string? FormUrl { get; set; }

        /// <summary>
        ///     Whether EasyPost has submitted the form to the carrier on behalf of the user.
        /// </summary>
        [JsonProperty("submitted_electronically")]
        public bool? SubmittedElectronically { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        internal Form()
        {
        }
    }
}
