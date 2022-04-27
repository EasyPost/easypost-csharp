using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class RefundCollection : Collection
    {
        [JsonProperty("refunds")]
        public List<Refund> refunds { get; set; }
    }
}
