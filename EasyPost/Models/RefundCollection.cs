using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class RefundCollection : Resource
    {
        [JsonProperty("refunds")]
        public List<Refund> refunds { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
