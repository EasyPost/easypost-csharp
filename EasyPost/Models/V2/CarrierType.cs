using System.Collections.Generic;
using EasyPost.ApiCompatibility.Migration;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class CarrierType : EasyPostObject, IMigratable
    {
        [JsonProperty("fields")]
        public Dictionary<string, object>? Fields { get; set; }
        [JsonProperty("logo")]
        public string? Logo { get; set; }
        [JsonIgnore]
        public MigrationGroup MigrationGroup => MigrationGroup.Sample;
        [JsonProperty("readable")]
        public string? Readable { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}
