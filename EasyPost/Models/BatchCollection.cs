using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class BatchCollection : Collection
    {
        [JsonProperty("batches")]
        public List<Batch> batches { get; set; }
    }
}
