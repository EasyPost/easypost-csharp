using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ReportCollection : Collection
    {
        [JsonProperty("reports")]
        public List<Report>? reports { get; set; }
        [JsonProperty("type")]
        public string? type { get; set; }
        public V2Client? V2Client { get; set; } // override the BaseClient property with a client property

        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.All().
        /// </summary>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        public async Task<ReportCollection> Next()
        {
            filters ??= new Dictionary<string, object>();
            if (reports != null)
            {
                filters["before_id"] = reports.Last().id ?? throw new PropertyMissing("reports");
            }

            if (V2Client == null)
            {
                throw new Exception("Client is null");
            }

            if (type != null)
            {
                return await V2Client.Reports.All(type, filters);
            }

            throw new PropertyMissing("type");
        }
    }
}
