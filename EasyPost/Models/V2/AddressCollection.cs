using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class AddressCollection : Collection
    {
        [JsonProperty("addresses")]
        public List<Address>? addresses { get; set; }
    }
}
