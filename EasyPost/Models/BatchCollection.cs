using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class BatchCollection : Resource
    {
        [JsonProperty("batches")]
        public List<Batch> batches { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
