using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class AddressCollection : Resource
    {
        [JsonProperty("addresses")]
        public List<Address> addresses { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
