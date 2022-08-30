using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API.Beta
{
    public class EndShipper : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("company")]
        public string company { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("street1")]
        public string street1 { get; set; }
        [JsonProperty("street2")]
        public string street2 { get; set; }
        [JsonProperty("zip")]
        public string zip { get; set; }

        #endregion

        #region CRUD Operations

        /// <summary>
        ///     Update this EndShipper. Must pass in all properties (new and existing).
        /// </summary>
        /// <param name="parameters">See EndShipper.Create for more details.</param>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("address");

            // EndShipper needs Put, not Patch
            await Update<EndShipper>(Method.Put, $"end_shippers/{id}", parameters, apiVersion: ApiVersion.Beta);
            return this;
        }

        #endregion
    }
}
