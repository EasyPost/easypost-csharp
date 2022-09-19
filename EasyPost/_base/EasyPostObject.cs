using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost._base
{
    /// <summary>
    ///     Class for any object that comes from or goes to the EasyPost API.
    /// </summary>
    public abstract class EasyPostObject : WithClient, IEasyPostObject
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; internal set; }
        [JsonProperty("id")]
        public string? Id { get; internal set; }
        [JsonProperty("mode")]
        public string? Mode { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }
        [JsonProperty("object")]
        internal string? Object { get; set; }

        #endregion

        internal string? Prefix
        {
            get { return Id?.Split('_').First(); }
        }

        public override bool Equals(object? obj)
        {
            if (GetType() != obj?.GetType())
            {
                return false;
            }

            string thisJson = AsJson();
            string otherJson = ((EasyPostObject)obj).AsJson();

            return thisJson == otherJson;
        }

        public override int GetHashCode()
        {
            return AsDictionary().GetHashCode();
        }

        /// <summary>
        ///     Update an EasyPostObject object server-side and in-place locally.
        /// </summary>
        /// <param name="method">HTTP method to use for update call.</param>
        /// <param name="url">Endpoint to hit for update call.</param>
        /// <param name="parameters">Parameters to include for update call.</param>
        /// <param name="rootElement">Root element of JSON returned by update call.</param>
        /// <param name="overrideApiVersion">Override the API version used for update call.</param>
        /// <typeparam name="T">Type of object to update.</typeparam>
        protected async Task Update<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null) where T : class
        {
            T updatedObject = await Request<T>(method, url, parameters, rootElement, overrideApiVersion);

            Merge(updatedObject);
        }

        /// <summary>
        ///     Get the dictionary representation of this object instance.
        /// </summary>
        /// <returns>A key-value dictionary representation of this object instance's attributes.</returns>
        private Dictionary<string, object?> AsDictionary() =>
            GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(info => info.Name, GetValue);

        /// <summary>
        ///     Get the JSON representation of this object instance.
        /// </summary>
        /// <returns>A JSON string representation of this object instance's attributes</returns>
        private string AsJson() => JsonSerialization.ConvertObjectToJson(this);

        private object? GetValue(PropertyInfo info)
        {
            object? value = info.GetValue(this, null);

            switch (value)
            {
                case EasyPostObject resource:
                    return resource.AsDictionary();
                case IEnumerable<EasyPostObject> enumerable:
                    List<Dictionary<string, object?>> values = enumerable.Select(resource => resource.AsDictionary()).ToList();
                    return values;
                default:
                    return value;
            }
        }

        /// <summary>
        ///     Merge the properties of this object instance with the properties of another object instance.
        ///     Adds properties from the input object instance into this object instance.
        /// </summary>
        /// <param name="source">Object instance to extract properties from to merge into this object instance.</param>
        private void Merge(object source)
        {
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(source, null), null);
            }
        }
    }

    public interface IEasyPostObject
    {
    }
}
