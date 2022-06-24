using EasyPost.Clients;
using Newtonsoft.Json;

namespace EasyPost.Interfaces
{
    public abstract class WithClient
    {
        [JsonIgnore]
        internal Client? Client { get; set; }
    }
}
