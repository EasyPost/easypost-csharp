using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class BatchCollection : Resource
    {
        [JsonProperty("batches")]
        public List<Batch> batches { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
