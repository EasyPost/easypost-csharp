using System;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class PostageLabel : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("date_advance")]
        public int date_advance { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("integrated_form")]
        public string integrated_form { get; set; }
        [JsonProperty("label_date")]
        public DateTime label_date { get; set; }
        [JsonProperty("label_epl2_url")]
        public string label_epl2_url { get; set; }
        [JsonProperty("label_file")]
        public string label_file { get; set; }
        [JsonProperty("label_file_type")]
        public string label_file_type { get; set; }
        [JsonProperty("label_pdf_url")]
        public string label_pdf_url { get; set; }
        [JsonProperty("label_resolution")]
        public int label_resolution { get; set; }
        [JsonProperty("label_size")]
        public string label_size { get; set; }
        [JsonProperty("label_type")]
        public string label_type { get; set; }
        [JsonProperty("label_url")]
        public string label_url { get; set; }
        [JsonProperty("label_zpl_url")]
        public string label_zpl_url { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
    }
}
