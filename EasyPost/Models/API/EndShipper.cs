using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class EndShipper : EasyPostObject, IEndShipperParameter
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

        /// <summary>
        ///     Update this <see cref="EndShipper"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.EndShippers.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="EndShipper"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(BetaFeatures.Parameters.EndShippers.Update parameters)
        {
            // EndShipper needs Put, not Patch
            await Update<EndShipper>(Method.Put, $"end_shippers/{Id}", parameters.ToDictionary());
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

        /// <summary>
        ///     Override the default BuildNextPageParameters method to build the parameters for the next page of EndShippers.
        /// </summary>
        /// <param name="entries">The entries of the <see cref="EndShipperCollection"/>.</param>
        /// <typeparam name="T">The type of <see cref="EasyPost._base.EasyPostObject"/> the entries are. Should be <see cref="EndShipper"/>.</typeparam>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of parameters to use for the subsequent API call.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there are no more items to retrieve for the paginated collection.</exception>
        internal override Dictionary<string, object> BuildNextPageParameters<T>(List<T>? entries)
        {
            if (entries is not List<EndShipper> endShippers)
            {
                throw new EndOfPaginationError();
            }

            if (endShippers == null || endShippers.Count == 0)
            {
                throw new EndOfPaginationError();
            }

            if (HasMore == null || !(bool)HasMore)
            {
                throw new EndOfPaginationError();
            }

            // TODO: How would we get the next page of EndShippers?

            throw new EndOfPaginationError();
        }
    }
}
