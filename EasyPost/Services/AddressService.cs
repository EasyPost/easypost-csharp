using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AddressService : EasyPostService
    {
        internal AddressService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an Address.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the address with. Valid pairs:
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
        ///     * {"verify", bool}
        ///     * {"verify_strict", bool}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        [CrudOperations.Create]
        public async Task<Address> Create(Dictionary<string, object> parameters)
        {
            // Check verify and verify_strict presence in parameters
            bool verify = parameters.ContainsKey("verify");
            bool verifyStrict = parameters.ContainsKey("verify_strict");

            // Clean and wrap parameters
            parameters.Remove("verify");
            parameters.Remove("verify_strict");
            parameters = parameters.Wrap("address");

            // Re-add verify and verify_strict if they were present, outside of the address wrapper
            // Verification is trigger by key presence, not key value, so only add the key if it's true.
            if (verify)
            {
                parameters.Add("verify", true);
            }

            if (verifyStrict)
            {
                parameters.Add("verify_strict", true);
            }

            return await Request<Address>(Method.Post, "addresses", parameters);
        }

        /// <summary>
        ///     Create an <see cref="Address"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Addresses.Create"/> parameter set.</param>
        /// <returns><see cref="Address"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Address> Create(BetaFeatures.Parameters.Addresses.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<Address>(Method.Post, "addresses", parameters.ToDictionary());
        }

        /// <summary>
        ///     Create and verify an Address.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the address with. Valid pairs:
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
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An Address object.</returns>
        [CrudOperations.Create]
        public async Task<Address> CreateAndVerify(Dictionary<string, object> parameters) => await Request<Address>(Method.Post, "addresses/create_and_verify", parameters, "address");

        /// <summary>
        ///     Create and verify an <see cref="Address"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Addresses.Create"/> parameter set.</param>
        /// <returns><see cref="Address"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Address> CreateAndVerify(BetaFeatures.Parameters.Addresses.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<Address>(Method.Post, "addresses/create_and_verify", parameters.ToDictionary(), "address");
        }

        /// <summary>
        ///     List all Address objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Address ID. Starts with "adr_". Only retrieve addresses created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Address ID. Starts with "adr". Only retrieve addresses created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve addresses created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve addresses created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.AddressCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<AddressCollection> All(Dictionary<string, object>? parameters = null) => await Request<AddressCollection>(Method.Get, "addresses", parameters);

        /// <summary>
        ///     List all <see cref="Address"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Addresses.All"/> parameter set.</param>
        /// <returns><see cref="AddressCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<AddressCollection> All(BetaFeatures.Parameters.Addresses.All parameters) => await Request<AddressCollection>(Method.Get, "addresses", parameters.ToDictionary());

        /// <summary>
        ///     Get the next page of a paginated <see cref="AddressCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="AddressCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="AddressCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<AddressCollection> GetNextPage(AddressCollection collection, int? pageSize = null) => await collection.GetNextPage<AddressCollection, BetaFeatures.Parameters.Addresses.All>(async parameters => await All(parameters), collection.Addresses, pageSize);

        /// <summary>
        ///     Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <returns>EasyPost.Address instance.</returns>
        [CrudOperations.Read]
        public async Task<Address> Retrieve(string id) => await Request<Address>(Method.Get, $"addresses/{id}");

        /// <summary>
        ///     Verify an Address.
        /// </summary>
        /// <param name="id">ID of the address to verify.</param>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        [CrudOperations.Update]
        public async Task<Address> Verify(string id)
        {
            return await Request<Address>(Method.Get, $"addresses/{id}/verify", null, "address");
        }

        #endregion
    }
}
