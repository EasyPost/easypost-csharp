using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class ShipmentCollection : PaginatedCollection
    {
        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("shipments")]
        public List<Shipment> shipments { get; set; }

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.All().
        /// </summary>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        public async Task<ShipmentCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = shipments.Last().id;

            return await Client.ShipmentService.All(filters);
        }
    }
}
