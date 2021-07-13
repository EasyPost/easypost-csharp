using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyPost
{
    public class VerificationDetails : Resource {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string time_zone { get; set; }

        public static new T Load<T>(string json) where T : Resource
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
