using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ShipmentCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.All().
        /// </summary>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(Shipments, "shipments");
            return await Client!.Shipments.All(Filters);
        }
    }
}
