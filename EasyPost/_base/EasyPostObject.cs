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
        public DateTime? created_at { get; internal set; }
        [JsonProperty("id")]
        public string? id { get; internal set; }
        [JsonProperty("mode")]
        public string? mode { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; internal set; }
        [JsonProperty("object")]
        internal string? Object { get; set; }

        #endregion

        internal string? Prefix
        {
            get
            {
                if (id == null)
                {
                    return null;
                }

                string? prefix = id.Split('_').First() ?? null;
                return prefix;
            }
        }

        public override bool Equals(object? obj)
        {
            if (GetType() != obj?.GetType())
            {
                return false;
            }

            string? thisJson = AsJson();
            string? otherJson = ((EasyPostObject)obj).AsJson();
            if (thisJson == null || otherJson == null)
            {
                // can't do proper comparison if either or both could not be serialized
                return false;
            }

            return thisJson == otherJson;
        }

        public override int GetHashCode()
        {
            return AsDictionary().GetHashCode();
        }

        /// <summary>
        ///     Merge the properties of this object instance with the properties of another object instance.
        ///     Adds properties from the input object instance into this object instance.
        /// </summary>
        /// <param name="source">Object instance to extract properties from to merge into this object instance.</param
        internal void Merge(object source)
        {
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(source, null), null);
            }
        }

        protected async Task Delete(string url, Dictionary<string, object>? parameters = null, ApiVersion? apiVersion = null)
        {
            await Request(Method.Delete, url, parameters, apiVersion);
        }

        protected async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class
        {
            if (Client == null)
            {
                throw new Exception("Client not configured");
            }

            return await Client.Request<T>(method, url, parameters, rootElement, apiVersion);
        }

        protected async Task Request(Method method, string url, Dictionary<string, object>? parameters = null, ApiVersion? apiVersion = null)
        {
            if (Client == null)
            {
                throw new Exception("Client not configured");
            }

            await Client.Request(method, url, parameters, apiVersion);
        }

        protected async Task Update<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class
        {
            T updatedObject = await Request<T>(method, url, parameters, rootElement, apiVersion);
            if (updatedObject == null)
            {
                throw new Exception("Failed to update object");
            }

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
    }

    public interface IEasyPostObject
    {
    }
}
