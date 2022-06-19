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
    public class ShipmentCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.All().
        /// </summary>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<IPaginatedCollection> Next()
        {
            Filters ??= new Dictionary<string, object>();
            Filters["before_id"] = (Shipments ?? throw new PropertyMissing("shipments")).Last().Id ?? throw new PropertyMissing("id");

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.Shipments.All(Filters);
        }
    }
}
