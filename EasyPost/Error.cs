using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace EasyPost {
    public class Error : Resource {
        public string code { get; set; }
        public string field { get; set; }
        public string suggestion { get; set; }
        public string message { get; set; }
        public List<Error> errors { get; set; }

        public static new T Load<T>(string json) where T : Resource {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
