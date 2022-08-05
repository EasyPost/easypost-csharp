using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ShipmentCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("filters")]
        public Dictionary<string, object?>? filters { get; set; }

        [JsonProperty("shipments")]
        public List<Shipment> shipments { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.All().
        /// </summary>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(shipments, "shipments");
            return await (Client as Client)!.Shipment.All(Filters);
        }
    }
}
