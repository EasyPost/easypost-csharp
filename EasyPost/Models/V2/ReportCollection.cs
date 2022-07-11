using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ReportCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("reports")]
        public List<Report>? Reports { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.All().
        /// </summary>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(Reports, "reports");
            return await Client!.Reports.All(Type!, Filters);
        }
    }
}
