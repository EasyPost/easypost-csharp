using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Newtonsoft.Json;

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

            return GetHashCode() == ((EasyPostObject)obj).GetHashCode();
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return AsJson().GetHashCode() ^ GetType().GetHashCode() ^ (Client != null ? Client!.GetHashCode() : 1);
        }

        public static bool operator ==(EasyPostObject? one, object? two)
        {
            if (one is null && two is null)
            {
                return true;
            }

            if (one is null || two is null)
            {
                return false;
            }

            return one.Equals(two);
        }

        public static bool operator !=(EasyPostObject? one, object? two)
        {
            return !(one == two);
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
        protected async Task Update<T>(Utilities.Http.Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null) where T : class
        {
            T updatedObject = await Request<T>(method, url, parameters, rootElement, overrideApiVersion);

            Merge(updatedObject);
        }

        /// <summary>
        ///     Get the JSON representation of this object instance.
        /// </summary>
        /// <returns>A JSON string representation of this object instance's attributes</returns>
        private string AsJson() => JsonSerialization.ConvertObjectToJson(this);

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
