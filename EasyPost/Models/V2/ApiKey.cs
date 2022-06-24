using EasyPost.ApiCompatibility.Migration;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ApiKey : EasyPostObject, IMigratable
    {
        [JsonProperty("key")]
        public string? Key { get; set; }
        [JsonIgnore]
        public MigrationGroup MigrationGroup => MigrationGroup.Sample;
    }
}
