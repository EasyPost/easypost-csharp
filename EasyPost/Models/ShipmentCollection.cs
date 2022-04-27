using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class ShipmentCollection : Collection
    {
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

            if (Client == null)
            {
                throw new Exception("Client is null");
            }

            return await Client.Shipments.All(filters);
        }
    }
}
