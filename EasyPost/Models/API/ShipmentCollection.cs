using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ShipmentCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }

        #endregion
    }
}
