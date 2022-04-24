using Newtonsoft.Json;

namespace EasyPost.Interfaces
{
    public class PaginatedCollection : Resource
    {
        [JsonIgnore]
        public ApiClient Client { get; set; }
    }
}
