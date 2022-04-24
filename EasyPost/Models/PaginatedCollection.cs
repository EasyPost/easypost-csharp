using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class PaginatedCollection : Resource
    {
        [JsonIgnore]
        public ApiClient Client { get; set; }
    }
}
