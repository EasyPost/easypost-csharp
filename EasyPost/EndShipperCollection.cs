using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class EndShipperCollection : Resource
    {
        #region JSON Properties

        [JsonProperty("end_shippers")]
        public List<EndShipper> end_shippers { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }

        #endregion
    }
}
