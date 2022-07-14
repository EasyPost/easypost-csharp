using EasyPost.Clients;
using Newtonsoft.Json;

namespace EasyPost._base
{
    public abstract class WithClient
    {
        [JsonIgnore]
        internal Client? Client { get; set; }
    }
}
