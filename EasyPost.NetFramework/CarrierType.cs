using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
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
        public static List<CarrierType> All()
        {
            Request request = new Request("carrier_types");
            return request.Execute<List<CarrierType>>();
        }
    }
}
