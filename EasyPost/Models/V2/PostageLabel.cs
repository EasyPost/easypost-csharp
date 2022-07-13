using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class PostageLabel : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("date_advance")]
        public int DateAdvance { get; set; }
        [JsonProperty("integrated_form")]
        public string? IntegratedForm { get; set; }
        [JsonProperty("label_date")]
        public DateTime LabelDate { get; set; }
        [JsonProperty("label_epl2_url")]
        public string? LabelEpl2Url { get; set; }
        [JsonProperty("label_file")]
        public string? LabelFile { get; set; }
        [JsonProperty("label_file_type")]
        public string? LabelFileType { get; set; }
        [JsonProperty("label_pdf_url")]
        public string? LabelPdfUrl { get; set; }
        [JsonProperty("label_resolution")]
        public int LabelResolution { get; set; }
        [JsonProperty("label_size")]
        public string? LabelSize { get; set; }
        [JsonProperty("label_type")]
        public string? LabelType { get; set; }
        [JsonProperty("label_url")]
        public string? LabelUrl { get; set; }
        [JsonProperty("label_zpl_url")]
        public string? LabelZplUrl { get; set; }

        #endregion
    }
}
