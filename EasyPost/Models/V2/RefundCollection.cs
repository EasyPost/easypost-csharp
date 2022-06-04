using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class RefundCollection : Collection
    {
        [JsonProperty("refunds")]
        public List<Refund>? refunds { get; set; }
    }
}
