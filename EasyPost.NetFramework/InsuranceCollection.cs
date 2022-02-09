using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class InsuranceCollection
    {
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("insurances")]
        public List<Insurance> insurances { get; set; }
    }
}
