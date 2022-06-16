using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class InsuranceCollection : Collection
    {
        [JsonProperty("insurances")]
        public List<Insurance>? Insurances { get; set; }
    }
}
