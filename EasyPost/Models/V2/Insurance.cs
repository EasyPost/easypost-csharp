using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
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

        /// <summary>
        ///     Refresh this Insurance.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters to use when refreshing this insurance.</param>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Insurance> Refresh(Parameters.V2.Insurance.Refresh? parameters = null)
        {
            return await Update<Insurance>(Method.Patch, $"insurances/{Id}", parameters);
        }
    }
}
