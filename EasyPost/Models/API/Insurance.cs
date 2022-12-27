using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Insurance : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public string? Amount { get; set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }
        [JsonProperty("messages")]
        public List<string>? Messages { get; set; }
        [JsonProperty("provider")]
        public string? Provider { get; set; }
        [JsonProperty("provider_id")]
        public string? ProviderId { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion

        internal Insurance()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Refresh this Insurance.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters to use when refreshing this insurance.</param>
        /// <returns>This refreshed EasyPost.Insurance object.</returns>
        [CrudOperations.Update]
        [Obsolete("Use the Retrieve method instead. This method will be removed in a future version.")]
        public async Task<Insurance> Refresh(Dictionary<string, object>? parameters = null)
        {
            await Update<Insurance>(Utilities.Http.Method.Get, $"insurances/{Id}");
            return this;
        }

        #endregion
    }

    public class InsuranceCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("insurances")]
        public List<Insurance>? Insurances { get; set; }

        #endregion

        internal InsuranceCollection()
        {
        }
    }
}
