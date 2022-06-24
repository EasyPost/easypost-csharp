using System.Collections.Generic;
using EasyPost.ApiCompatibility.Migration;
using EasyPost.Interfaces;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class AddressCollection : Collection, IMigratable
    {
        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; set; }
        [JsonIgnore]
        public MigrationGroup MigrationGroup => MigrationGroup.Sample;
    }
}
