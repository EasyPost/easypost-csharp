using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Interfaces;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ShipmentCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("shipments")]
        public List<Shipment>? shipments { get; set; }

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.All().
        /// </summary>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<IPaginatedCollection> Next()
        {
            filters ??= new Dictionary<string, object>();
            filters["before_id"] = (shipments ?? throw new PropertyMissing("shipments")).Last().id ?? throw new PropertyMissing("id");

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.Shipments.All(filters);
        }
    }
}
