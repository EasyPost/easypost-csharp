using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class EndShipperCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("end_shippers")]
        public List<EndShipper>? EndShippers { get; set; }

        #endregion

        internal EndShipperCollection()
        {
        }
    }
}
