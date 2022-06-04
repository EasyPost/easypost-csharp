using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Interfaces
{
    public class Resource
    {
        [JsonIgnore] internal BaseClient Client;

        public override bool Equals(object? obj)
        {
            if (GetType() != obj?.GetType())
            {
                return false;
            }

            string? thisJson = AsJson();
            string? otherJson = ((Resource)obj).AsJson();
            if (thisJson == null || otherJson == null)
            {
                // can't do proper comparison if either or both could not be serialized
                return false;
            }

            return thisJson == otherJson;
        }

        public override int GetHashCode() => Client.GetHashCode();

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
        private string? AsJson() => JsonSerialization.ConvertObjectToJson(this);

        protected async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            if (Client == null)
            {
                throw new Exception("Client is null");
            }

            return await Client.Request<T>(method, url, parameters, rootElement);
        }

        protected async Task<bool> Request(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null)
        {
            if (Client == null)
            {
                throw new Exception("Client is null");
            }

            return await Client.Request(method, url, parameters, rootElement);
        }

        protected async Task Update<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            T updatedObject = await Request<T>(method, url, parameters, rootElement);
            if (updatedObject == null)
            {
                throw new Exception("Failed to update object");
            }

            Merge(updatedObject);
        }

        private object? GetValue(PropertyInfo info)
        {
            object? value = info.GetValue(this, null);

            switch (value)
            {
                case Resource resource:
                    return resource.AsDictionary();
                case IEnumerable<Resource> enumerable:
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
}
