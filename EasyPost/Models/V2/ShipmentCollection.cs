using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ShipmentCollection : Collection
    {
        [JsonProperty("shipments")]
        public List<Shipment> shipments { get; set; }

        public V2Client V2Client { get; set; } // override the BaseClient property with a client property

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.All().
        /// </summary>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        public async Task<ShipmentCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = shipments.Last().id;

            if (V2Client == null)
            {
                throw new Exception("Client is null");
            }

            return await V2Client.Shipments.All(filters);
        }
    }
}
