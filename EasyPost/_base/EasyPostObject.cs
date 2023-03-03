using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    /// <summary>
    ///     Class for any object that comes from or goes to the EasyPost API.
    /// </summary>
    public abstract class EasyPostObject : EphemeralEasyPostObject
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; internal set; }
        [JsonProperty("id")]
        public string? Id { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        #endregion

        internal string? Prefix => Id?.Split('_').First();

        public override bool Equals(object? obj) => GetType() == obj?.GetType() && GetHashCode() == ((EasyPostObject)obj).GetHashCode();

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = "Client is used to determine equality.")]
#pragma warning disable CA1307
        public override int GetHashCode() => AsJson().GetHashCode() ^ GetType().GetHashCode() ^ (Client != null ? Client!.GetHashCode() : 1);
#pragma warning restore CA1307

        public static bool operator ==(EasyPostObject? one, object? two)
        {
            if (one is null && two is null)
            {
                return true;
            }

#pragma warning disable IDE0046
            if (one is null || two is null)
#pragma warning restore IDE0046
            {
                return false;
            }

            return one.Equals(two);
        }

        public static bool operator !=(EasyPostObject? one, object? two) => !(one == two);

        /// <summary>
        ///     Update an EasyPostObject object server-side and in-place locally.
        /// </summary>
        /// <param name="method">HTTP method to use for update call.</param>
        /// <param name="url">Endpoint to hit for update call.</param>
        /// <param name="parameters">Parameters to include for update call.</param>
        /// <param name="rootElement">Root element of JSON returned by update call.</param>
        /// <param name="overrideApiVersion">Override the API version used for update call.</param>
        /// <typeparam name="T">Type of object to update.</typeparam>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected async Task Update<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null)
            where T : class
        {
            T updatedObject = await Request<T>(method, url, parameters, rootElement, overrideApiVersion);

            Merge(updatedObject);
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
