using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class RefundCollection : Resource
    {
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("refunds")]
        public List<Refund> refunds { get; set; }
    }
}
