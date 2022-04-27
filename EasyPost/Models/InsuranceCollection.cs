using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class InsuranceCollection : Collection
    {
        [JsonProperty("insurances")]
        public List<Insurance> insurances { get; set; }
    }
}
