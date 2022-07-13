using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Interfaces
{
    public abstract class EasyPostObject : WithClient, IEasyPostObject
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; internal set; }
        [JsonProperty("id")]
        public string? Id { get; internal set; }
        [JsonProperty("mode")]
        public string? Mode { get; internal set; }
        [JsonProperty("object")]
        internal string? Object { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        #endregion

        internal string? Prefix
        {
            get
            {
                if (Id == null)
                {
                    return null;
                }

                string? prefix = Id.Split('_').First() ?? null;
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

        protected async Task<T> Request<T>(Method method, string url, EasyPostParameters? parameters = null, string? rootElement = null) where T : class
        {
            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.Request<T>(method, url, parameters, rootElement);
        }

        protected async Task<bool> Request(Method method, string url, EasyPostParameters? parameters = null, string? rootElement = null)
        {
            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.Request(method, url, parameters, rootElement);
        }

        protected async Task<T> Update<T>(Method method, string url, EasyPostParameters? parameters = null, string? rootElement = null) where T : class
        {
            T updatedObject = await Request<T>(method, url, parameters, rootElement);
            if (updatedObject == null)
            {
                throw new ObjectException("Failed to update object");
            }

            return updatedObject;
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

    internal interface IEasyPostObject
    {
    }
}
