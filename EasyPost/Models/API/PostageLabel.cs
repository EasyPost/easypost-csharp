using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#postage-label-object">EasyPost postage label</a>.
    /// </summary>
    public class PostageLabel : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("date_advance")]
        public int? DateAdvance { get; set; }

        [JsonProperty("integrated_form")]
        public string? IntegratedForm { get; set; }

        /// <summary>
        ///     The date that appears on the postage label.
        /// </summary>
        [JsonProperty("label_date")]
        public DateTime? LabelDate { get; set; }

        /// <summary>
        ///     The URL of the EPL2 label file.
        /// </summary>
        [JsonProperty("label_epl2_url")]
        public string? LabelEpl2Url { get; set; }

        [JsonProperty("label_file")]
        public string? LabelFile { get; set; }

        /// <summary>
        ///     The file type of the label.
        /// </summary>
        [JsonProperty("label_file_type")]
        public string? LabelFileType { get; set; }

        /// <summary>
        ///     The URL of the PDF label file.
        /// </summary>
        [JsonProperty("label_pdf_url")]
        public string? LabelPdfUrl { get; set; }

        /// <summary>
        ///     The resolution of the label.
        /// </summary>
        [JsonProperty("label_resolution")]
        public int? LabelResolution { get; set; }

        /// <summary>
        ///     The size of the label.
        /// </summary>
        [JsonProperty("label_size")]
        public string? LabelSize { get; set; }

        /// <summary>
        ///     The type of the label.
        /// </summary>
        [JsonProperty("label_type")]
        public string? LabelType { get; set; }

        /// <summary>
        ///     The URL of the label file.
        /// </summary>
        [JsonProperty("label_url")]
        public string? LabelUrl { get; set; }

        /// <summary>
        ///     The URL of the ZPL label file.
        /// </summary>
        [JsonProperty("label_zpl_url")]
        public string? LabelZplUrl { get; set; }

        #endregion

    }
}
