using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyPost {
    public class Error : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public string code { get; set; }
        public string field { get; set; }
        public string suggestion { get; set; }
        public string message { get; set; }
        public List<Error> errors { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        public static new T Load<T>(string json) where T : Resource {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}