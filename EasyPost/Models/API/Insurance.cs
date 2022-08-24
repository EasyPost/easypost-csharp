using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Insurance : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public string amount { get; set; }
        [JsonProperty("from_address")]
        public Address from_address { get; set; }
        [JsonProperty("messages")]
        public List<string> messages { get; set; }
        [JsonProperty("provider")]
        public string provider { get; set; }
        [JsonProperty("provider_id")]
        public string provider_id { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("to_address")]
        public Address to_address { get; set; }
        [JsonProperty("tracker")]
        public Tracker tracker { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }

        #endregion

        #region CRUD Operations

        /// <summary>
        ///     Refresh this Insurance.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters to use when refreshing this insurance.</param>
        /// <returns>This refreshed EasyPost.Insurance object.</returns>
        [CrudOperations.Update]
        public async Task<Insurance> Refresh(Dictionary<string, object>? parameters = null)
        {
            return await Update<Insurance>(Method.Patch, $"insurances/{id}", parameters);
        }

        #endregion
    }
}
