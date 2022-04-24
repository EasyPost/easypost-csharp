using Newtonsoft.Json;

namespace EasyPost.Interfaces
{
    public class PaginatedCollection : Resource
    {
        [JsonIgnore]
        public Client Client { get; set; }
    }
}
