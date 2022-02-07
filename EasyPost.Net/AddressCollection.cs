using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class AddressCollection : Resource
    {
        [JsonProperty("addresses")]
        public List<Address> addresses { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
