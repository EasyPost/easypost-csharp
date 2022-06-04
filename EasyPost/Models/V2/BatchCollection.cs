using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class BatchCollection : Collection
    {
        [JsonProperty("batches")]
        public List<Batch>? batches { get; set; }
    }
}
