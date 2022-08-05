
using Newtonsoft.Json;

namespace EasyPost._base
{
    public abstract class WithClient
    {
        [JsonIgnore]
        internal EasyPostClient? Client { get; set; }
    }
}
