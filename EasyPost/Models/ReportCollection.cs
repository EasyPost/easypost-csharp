using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class ReportCollection : Collection
    {
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

            if (Client == null)
            {
                throw new Exception("Client is null");
            }

            return await Client.Reports.All(type, filters);
        }
    }
}
