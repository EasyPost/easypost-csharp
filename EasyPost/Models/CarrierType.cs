using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
{
    public class CarrierType : Resource
    {
        [JsonProperty("fields")]
        public Dictionary<string, object> fields { get; set; }
        [JsonProperty("logo")]
        public string logo { get; set; }
        [JsonProperty("readable")]
        public string readable { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        ///     Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>
        public static async Task<List<CarrierType>> All()
        {
            Request request = new Request("carrier_types", Method.Get);
            return await request.Execute<List<CarrierType>>();
        }
    }
}
