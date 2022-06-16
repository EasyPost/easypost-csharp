using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ReportCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("reports")]
        public List<Report>? Reports { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.All().
        /// </summary>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<IPaginatedCollection> Next()
        {
            Filters ??= new Dictionary<string, object>();
            if (Reports != null)
            {
                Filters["before_id"] = Reports.Last().Id ?? throw new PropertyMissing("reports");
            }

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            if (Type != null)
            {
                return await Client.Reports.All(Type, Filters);
            }

            throw new PropertyMissing("type");
        }
    }
}
