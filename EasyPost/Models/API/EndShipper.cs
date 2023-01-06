using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class EndShipper : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("company")]
        public string? Company { get; set; }
        [JsonProperty("country")]
        public string? Country { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("error")]
        public string? Error { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("phone")]
        public string? Phone { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("street1")]
        public string? Street1 { get; set; }
        [JsonProperty("street2")]
        public string? Street2 { get; set; }
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        internal EndShipper()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Update this EndShipper. Must pass in all properties (new and existing).
        /// </summary>
        /// <param name="parameters">See EndShipper.Create for more details.</param>
        /// <returns>The updated EndShipper.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("address");

            // EndShipper needs Put, not Patch
            await Update<EndShipper>(Method.Put, $"end_shippers/{Id}", parameters);
            return this;
        }

        #endregion
    }

    public class EndShipperCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("end_shippers")]
        public List<EndShipper>? EndShippers { get; set; }

        #endregion

        internal EndShipperCollection()
        {
        }
    }
}
