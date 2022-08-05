using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ReportCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("reports")]
        public List<Report> reports { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.All().
        /// </summary>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(reports, "reports");
            return await (Client as Client)!.Report.All(type!, Filters);
        }
    }
}
