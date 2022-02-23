using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EasyPost
{
    public class ScanFormList : Resource
    {
        [JsonProperty("filters")]
        public Dictionary<string, object> filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("scan_forms")]
        public List<ScanForm> scan_forms { get; set; }

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.List().
        /// </summary>
        /// <returns>A new EasyPost.ScanFormList instance.</returns>
        public ScanFormList Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = scan_forms.Last().id;

            return ScanForm.List(filters);
        }
    }
}
