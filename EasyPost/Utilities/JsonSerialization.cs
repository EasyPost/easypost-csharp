using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EasyPost.Utilities
{
    /// <summary>
    /// Instance of a JSON de/serializer with custom serialization settings
    /// </summary>
    internal class JsonSerializer
    {
        private JsonSerializerSettings JsonSerializerSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="jsonSerializerSettings">Custom <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to override default built-in settings.</param>
        internal JsonSerializer(JsonSerializerSettings? jsonSerializerSettings = null)
        {
            JsonSerializerSettings = jsonSerializerSettings ?? JsonSerialization.DefaultJsonSerializerSettings;
        }

        /// <summary>
        /// Deserialize a JSON string into a dynamic object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>An ExpandoObject object</returns>
        internal ExpandoObject ConvertJsonToObject(string data, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject(data, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize a JSON string into a T-type object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        internal T ConvertJsonToObject<T>(string data, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject<T>(data, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a dynamic object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>An ExpandoObject object</returns>
        internal ExpandoObject ConvertJsonToObject(RestResponse response, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject(response, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a T-type object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        internal T ConvertJsonToObject<T>(RestResponse response, List<string>? rootElementKeys = null) => JsonSerialization.ConvertJsonToObject<T>(response, JsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Serialize an object into a JSON string, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">An object to serialize into a string</param>
        /// <returns>A string of JSON data</returns>
        internal string? ConvertObjectToJson(object data) => JsonSerialization.ConvertObjectToJson(data, JsonSerializerSettings);
    }

    /// <summary>
    /// JSON de/serialization utilities
    /// </summary>
    internal static class JsonSerialization
    {
        /// <summary>
        /// The default <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for de/serialization
        /// </summary>
        internal static JsonSerializerSettings DefaultJsonSerializerSettings
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
        /// Deserialize a JSON string into a T-type object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        internal static T ConvertJsonToObject<T>(string? data, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
        {
            if (rootElementKeys != null && rootElementKeys.Any())
            {
                data = GoToRootElement(data, rootElementKeys);
            }

            if (data == null || string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("No data to deserialize");
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
        /// Deserialize a JSON string into a dynamic object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>An ExpandoObject object</returns>
        internal static ExpandoObject ConvertJsonToObject(string? data, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject<ExpandoObject>(data, jsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a T-type object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        internal static T ConvertJsonToObject<T>(RestResponse response, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject<T>(response.Content, jsonSerializerSettings, rootElementKeys);


        /// <summary>
        /// Deserialize data from a RestSharp.RestResponse into a dynamic object, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for deserialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>An ExpandoObject object</returns>
        internal static ExpandoObject ConvertJsonToObject(RestResponse response, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject(response.Content, jsonSerializerSettings, rootElementKeys);

        /// <summary>
        /// Serialize an object into a JSON string, using this instance's <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="data">An object to serialize into a string</param>
        /// <param name="jsonSerializerSettings">The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> to use for serialization. Defaults to <see cref="DefaultJsonSerializerSettings"/> if not provided.</param>
        /// <returns>A string of JSON data</returns>
        internal static string? ConvertObjectToJson(object data, JsonSerializerSettings? jsonSerializerSettings = null)
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
        /// Get the value of an ExpandoObject object property
        /// </summary>
        /// <param name="obj">ExpandoObject object</param>
        /// <param name="propertyName">Name of the property to get</param>
        /// <returns>An object or null</returns>
        internal static object? GetValueOfExpandoObjectProperty(ExpandoObject obj, string propertyName)
        {
            IDictionary<string, object> propertyValues = obj;
            return propertyValues.TryGetValue(propertyName, out object? value) ? value : null;
        }

        /// <summary>
        /// Get the value of a JObject object key
        /// </summary>
        /// <param name="obj">JObject object</param>
        /// <param name="key">Key of the JSON element to retrieve a value from</param>
        /// <returns>A T-type object</returns>
        internal static T GetValueOfJsonObjectKey<T>(JObject obj, string key)
        {
            return obj.TryGetValue(key, out JToken? value) ? value.ToObject<T>() : default;
        }

        /// <summary>
        /// Venture through the root element keys to find the root element of the JSON string.
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>The value of the JSON sub-element key path</returns>
        private static string? GoToRootElement(string? data, List<string> rootElementKeys)
        {
            if (data == null)
            {
                return null;
            }

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
