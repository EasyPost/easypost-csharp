using EasyPost.Interfaces;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class VerificationDetails : Resource
    {
        [JsonProperty("latitude")]
        public float latitude { get; set; }
        [JsonProperty("longitude")]
        public float longitude { get; set; }
        [JsonProperty("time_zone")]
        public string time_zone { get; set; }

        /// <summary>
        ///     Deserialize JSON data into an object instance.
        /// </summary>
        /// <param name="json">JSON data to use for object creation.</param>
        /// <typeparam name="T">Type of object to generate.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static new T Load<T>(string json) where T : Resource => JsonSerialization.ConvertJsonToObject<T>(json);
    }
}
