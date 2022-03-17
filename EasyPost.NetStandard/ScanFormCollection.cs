using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EasyPost
{
    public class ScanFormCollection : Resource
    {
        [JsonProperty("filters")]
        public Dictionary<string, object> filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("scan_forms")]
        public List<ScanForm> scan_forms { get; set; }

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        public ScanFormCollection Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = scan_forms.Last().id;

            return ScanForm.All(filters);
        }
    }
}
