using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EasyPost.Utilities
{
    /// <summary>
    /// Instance of a JSON de/serializer with custom serialization settings
    /// </summary>
    public class JsonSerializer
    {
        private JsonSerializerSettings JsonSerializerSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="jsonSerializerSettings">Custom <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to override default built-in settings.</param>
        public JsonSerializer(JsonSerializerSettings? jsonSerializerSettings = null)
        {
            JsonSerializerSettings = jsonSerializerSettings ?? JsonSerialization.DefaultJsonSerializerSettings;
        }

        /// <summary>
        /// Deserialize a JSON string into a dynamic object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>A dynamic object</returns>
        public dynamic ConvertJsonToObject(string data, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject(data, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize a JSON string into a T-type object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        public T ConvertJsonToObject<T>(string data, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject<T>(data, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a dynamic object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>A dynamic object</returns>
        public dynamic ConvertJsonToObject(RestResponse response, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject(response, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a T-type object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        public T ConvertJsonToObject<T>(RestResponse response, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject<T>(response, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Serialize an object into a JSON string, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">An object to serialize into a string</param>
        /// <returns>A string of JSON data</returns>
        public string? ConvertObjectToJson(object data) => JsonSerialization.ConvertObjectToJson(data, JsonSerializerSettings);
    }

    /// <summary>
    /// JSON de/serialization utilities
    /// </summary>
    public static class JsonSerialization
    {
        /// <summary>
        /// The default <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for de/serialization
        /// </summary>
        public static JsonSerializerSettings DefaultJsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };
            }
        }

        /// <summary>
        /// Deserialize a JSON string into a dynamic object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic ConvertJsonToObject(string? data, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
        {
            if (rootElementKeys != null && rootElementKeys.Any())
            {
                data = GoToRootElement(data, rootElementKeys);
            }

            return JsonConvert.DeserializeObject(data, jsonSerializerSettings ?? DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Deserialize a JSON string into a T-type object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        public static T ConvertJsonToObject<T>(string? data, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
        {
            if (rootElementKeys != null && rootElementKeys.Any())
            {
                data = GoToRootElement(data, rootElementKeys);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(data, jsonSerializerSettings ?? DefaultJsonSerializerSettings);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a dynamic object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic ConvertJsonToObject(RestResponse response, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject(response.Content, jsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a T-type object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        public static T ConvertJsonToObject<T>(RestResponse response, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject<T>(response.Content, jsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Serialize an object into a JSON string, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">An object to serialize into a string</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for serialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <returns>A string of JSON data</returns>
        public static string? ConvertObjectToJson(object data, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            try
            {
                return JsonConvert.SerializeObject(data, jsonSerializerSettings ?? DefaultJsonSerializerSettings);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Venture through the root element keys to find the root element of the JSON string.
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>The value of the JSON sub-element key path</returns>
        private static string? GoToRootElement(string? data, List<string> rootElementKeys)
        {
            object json = JsonConvert.DeserializeObject(data);
            try
            {
                rootElementKeys.ForEach(key => { json = (json as JObject).Property(key).Value; });
                return (json as JToken).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
