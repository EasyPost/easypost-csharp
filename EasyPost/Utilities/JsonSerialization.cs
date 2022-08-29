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
    ///     JSON de/serialization utilities
    /// </summary>
    internal static class JsonSerialization
    {
        /// <summary>
        ///     The default <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for de/serialization
        /// </summary>
        private static JsonSerializerSettings DefaultJsonSerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        /// <summary>
        ///     Deserialize a JSON string into a T-type object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">
        ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
        ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
        /// </param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        internal static T ConvertJsonToObject<T>(string? data, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
        {
            object obj = ConvertJsonToObject(data, typeof(T), jsonSerializerSettings, rootElementKeys);
            if (obj is T t)
            {
                return t;
            }

            throw new Exception($"Could not deserialize JSON data into type {typeof(T).Name}");
        }

        /// <summary>
        ///     Deserialize a JSON string into a T-type object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">
        ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
        ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
        /// </param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <param name="type">Type of object to deserialize to</param>
        /// <returns>A T-type object</returns>
        internal static object ConvertJsonToObject(string? data, Type type, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
        {
            if (rootElementKeys != null && rootElementKeys.Any())
            {
                data = GoToRootElement(data, rootElementKeys);
            }

            if (data == null || string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("No data to deserialize.");
            }

            try
            {
                object? obj = JsonConvert.DeserializeObject(data, type, jsonSerializerSettings ?? DefaultJsonSerializerSettings);
                return (obj ?? default)!;
            }
            catch (Exception)
            {
                throw new Exception("Could not deserialize data.");
            }
        }

        /// <summary>
        ///     Deserialize a JSON string into a dynamic object
        /// </summary>
        /// <param name="data">A string of JSON data</param>
        /// <param name="jsonSerializerSettings">
        ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
        ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
        /// </param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <returns>An ExpandoObject object</returns>
        internal static ExpandoObject ConvertJsonToObject(string? data, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject<ExpandoObject>(data, jsonSerializerSettings, rootElementKeys);

        /// <summary>
        ///     Deserialize data from a RestSharp.RestResponse into a T-type object, using this instance's
        ///     <see cref="JsonSerializerSettings" />
        /// </summary>
        /// <param name="response">RestSharp.RestResponse object to extract data from.</param>
        /// <param name="jsonSerializerSettings">
        ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
        ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
        /// </param>
        /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
        /// <typeparam name="T">Type of object to deserialize to</typeparam>
        /// <returns>A T-type object</returns>
        internal static T ConvertJsonToObject<T>(RestResponse response, JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) => ConvertJsonToObject<T>(response.Content, jsonSerializerSettings, rootElementKeys);

        /// <summary>
        ///     Serialize an object into a JSON string, using this instance's <see cref="JsonSerializerSettings" />
        /// </summary>
        /// <param name="data">An object to serialize into a string</param>
        /// <param name="jsonSerializerSettings">
        ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
        ///     serialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
        /// </param>
        /// <returns>A string of JSON data</returns>
        internal static string ConvertObjectToJson(object data, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            try
            {
                return JsonConvert.SerializeObject(data, jsonSerializerSettings ?? DefaultJsonSerializerSettings);
            }
            catch (Exception)
            {
                throw new Exception("Could not serialize object.");
            }
        }

        /// <summary>
        ///     Venture through the root element keys to find the root element of the JSON string.
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

            object? json = JsonConvert.DeserializeObject(data);
            try
            {
                rootElementKeys.ForEach(key => { json = (json as JObject)?.Property(key)?.Value; });
                return (json as JToken)?.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
