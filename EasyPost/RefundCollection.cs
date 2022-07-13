using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class RefundCollection : Resource
    {
        #region JSON Properties

        [JsonProperty("refunds")]
        public List<Refund> refunds { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }

        #endregion
    }
}
