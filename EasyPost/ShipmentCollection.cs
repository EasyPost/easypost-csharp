using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost
{
    public class ShipmentCollection : Resource
    {
        [JsonProperty("filters")]
        public Dictionary<string, object> filters { get; set; }
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

            return await Shipment.All(filters);
        }
    }
}
