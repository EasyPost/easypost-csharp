using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class InsuranceCollection : Resource
    {
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("insurances")]
        public List<Insurance> insurances { get; set; }
    }
}
