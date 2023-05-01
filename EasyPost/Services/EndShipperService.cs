using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    public class EndShipperService : EasyPostService
    {
        internal EndShipperService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an EndShipper.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the EndShipper with. Valid pairs:
        ///     * {"name", string}
        ///     * {"company", string}
        ///     * {"street1", string}
        ///     * {"street2", string}
        ///     * {"city", string}
        ///     * {"state", string}
        ///     * {"zip", string}
        ///     * {"country", string}
        ///     * {"phone", string}
        ///     * {"email", string}
        ///     * {"verifications", List&lt;string&gt;} Possible items are "delivery" and "zip4".
        ///     * {"strict_verifications", List&lt;string&gt;} Possible items are "delivery" and "zip4".
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.EndShipper instance.</returns>
        [CrudOperations.Create]
        public async Task<EndShipper> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("address");
            return await Request<EndShipper>(Method.Post, "end_shippers", parameters);
        }

        /// <summary>
        ///     Create an <see cref="EndShipper"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.EndShippers.Create"/> parameter set.</param>
        /// <returns><see cref="EndShipper"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<EndShipper> Create(BetaFeatures.Parameters.EndShippers.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<EndShipper>(Method.Post, "end_shippers", parameters.ToDictionary());
        }

        /// <summary>
        ///     List all EndShipper objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an EndShipper ID. Starts with "es_". Only retrieve EndShippers created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an EndShipper ID. Starts with "es". Only retrieve EndShippers created
        ///     after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.EndShipperCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipperCollection> All(Dictionary<string, object>? parameters = null)
        {
            EndShipperCollection collection = await Request<EndShipperCollection>(Method.Get, "end_shippers", parameters);
            collection.Filters = BaseAllParameters.FromDictionary<BetaFeatures.Parameters.EndShippers.All>(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="EndShipper"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.EndShippers.All"/> parameter set.</param>
        /// <returns><see cref="EndShipperCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipperCollection> All(BetaFeatures.Parameters.EndShippers.All parameters) => await All(parameters.ToDictionary());

        // TODO: Add GetNextPage function when "before_id" available for EndShipper All endpoint.

        /// <summary>
        ///     Retrieve an EndShipper from its id.
        /// </summary>
        /// <param name="id">String representing an EndShipper. Starts with "es_".</param>
        /// <returns>EasyPost.EndShipper instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipper> Retrieve(string id) => await Request<EndShipper>(Method.Get, $"end_shippers/{id}");

        /// <summary>
        ///     Update this EndShipper. Must pass in all properties (new and existing).
        /// </summary>
        /// <param name="parameters">See EndShipper.Create for more details.</param>
        /// <returns>The updated EndShipper.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(string id, Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("address");

            return await Request<EndShipper>(Method.Put, $"end_shippers/{id}", parameters);
        }

        /// <summary>
        ///     Update this <see cref="EndShipper"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.EndShippers.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="EndShipper"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(string id, BetaFeatures.Parameters.EndShippers.Update parameters)
        {
            return await Request<EndShipper>(Method.Put, $"end_shippers/{id}", parameters.ToDictionary());
        }

        #endregion
    }
}
