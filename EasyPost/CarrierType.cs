using System.Collections.Generic;

namespace EasyPost {
    public class CarrierType : Resource {
        public string type { get; set; }
        public string readable { get; set; }
        public string logo { get; set; }
        public Dictionary<string, object> fields { get; set; }

        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public static List<CarrierType> All(string apiKey = null) {
            Request request = new Request("carrier_types");
            return request.Execute<List<CarrierType>>(apiKey);
        }
    }
}