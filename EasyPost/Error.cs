using System.Collections.Generic;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Error : Resource
    {
        #region JSON Properties

        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("errors")]
        public List<Error> errors { get; set; }
        [JsonProperty("field")]
        public string field { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("suggestion")]
        public string suggestion { get; set; }

        #endregion

        /// <summary>
        ///     Create an Error from JSON.
        /// </summary>
        /// <param name="json">JSON data to use for Error creation.</param>
        /// <typeparam name="T">The type of object to deserialize the JSON data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static new T Load<T>(string json) where T : Resource => JsonSerialization.ConvertJsonToObject<T>(json);
    }
}
