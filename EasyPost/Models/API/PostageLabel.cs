using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class PostageLabel : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("date_advance")]
        public int? DateAdvance { get; internal set; }
        [JsonProperty("integrated_form")]
        public string? IntegratedForm { get; internal set; }
        [JsonProperty("label_date")]
        public DateTime? LabelDate { get; internal set; }
        [JsonProperty("label_epl2_url")]
        public string? LabelEpl2Url { get; internal set; }
        [JsonProperty("label_file")]
        public string? LabelFile { get; internal set; }
        [JsonProperty("label_file_type")]
        public string? LabelFileType { get; internal set; }
        [JsonProperty("label_pdf_url")]
        public string? LabelPdfUrl { get; internal set; }
        [JsonProperty("label_resolution")]
        public int? LabelResolution { get; internal set; }
        [JsonProperty("label_size")]
        public string? LabelSize { get; internal set; }
        [JsonProperty("label_type")]
        public string? LabelType { get; internal set; }
        [JsonProperty("label_url")]
        public string? LabelUrl { get; internal set; }
        [JsonProperty("label_zpl_url")]
        public string? LabelZplUrl { get; internal set; }

        #endregion

        internal PostageLabel()
        {
        }
    }
}
