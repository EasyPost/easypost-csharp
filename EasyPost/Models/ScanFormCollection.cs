using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class ScanFormCollection : PaginatedCollection
    {
        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("scan_forms")]
        public List<ScanForm> scan_forms { get; set; }

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        public async Task<ScanFormCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = scan_forms.Last().id;

            return await Client.ScanForms.All(filters);
        }
    }
}
