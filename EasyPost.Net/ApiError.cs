using System.Collections.Generic;
using System.Linq;
using EasyPost.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPost
{
    public class Suggestion
    {
        public string field { get; set; }
        public string problem { get; set; }

        public Suggestion(string field, string problem)
        {
            this.field = field;
            this.problem = problem;
        }
    }

    public class ApiError : Resource
    {
        [JsonProperty("code")]
        public string? code { get; set; }
        public List<Suggestion>? suggestions { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }

        // throws error if can't parse JSON correctly
        public ApiError(string json)
        {
            dynamic tempObject = JsonSerialization.ConvertJsonToObject(json);
            code = tempObject.error.code;
            message = tempObject.error.message;
            if (tempObject.error.errors != null)
            {
                suggestions = new List<Suggestion>();
                foreach (JObject error in tempObject.error.errors)
                {
                    Dictionary<string, object> errorDict = error.ToObject<Dictionary<string, object>>();
                    foreach (KeyValuePair<string, object> errorDictEntry in errorDict)
                    {
                        suggestions.Add(new Suggestion(errorDictEntry.Key, errorDictEntry.Value.ToString()));
                    }
                }
            }
            else
            {
                suggestions = null;
            }
        }

        public ApiError(string code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
