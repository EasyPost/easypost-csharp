using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class AddressCollection : Collection
    {
        [JsonProperty("addresses")]
        public List<Address> addresses { get; set; }
    }
}
