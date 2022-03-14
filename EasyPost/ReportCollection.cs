using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost
{
    public class ReportCollection : Resource
    {
        [JsonProperty("filters")]
        public Dictionary<string, object> filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("reports")]
        public List<Report> reports { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.All().
        /// </summary>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        public async Task<ReportCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = reports.Last().id;

            return await Report.All(type, filters);
        }
    }
}
