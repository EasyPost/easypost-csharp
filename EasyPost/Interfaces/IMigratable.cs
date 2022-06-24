using EasyPost.ApiCompatibility.Migration;
using Newtonsoft.Json;

namespace EasyPost.Interfaces
{
    public interface IMigratable
    {
        [JsonIgnore]
        public MigrationGroup MigrationGroup { get; }
    }
}
