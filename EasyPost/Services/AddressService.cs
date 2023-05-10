using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#addresses">address-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AddressService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AddressService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal AddressService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an <see cref="Address"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-and-verify-addresses">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Address"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Address"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Address> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
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

            return await RequestAsync<Address>(Method.Post, "addresses", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create an <see cref="Address"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-and-verify-addresses">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Address"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Address"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Address> Create(Parameters.Address.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Address>(Method.Post, "addresses", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Create and verify an <see cref="Address"/> in one API call.
        ///     <a href="https://www.easypost.com/docs/api#create-and-verify-addresses">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Address"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Address"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Address> CreateAndVerify(Dictionary<string, object> parameters, CancellationToken cancellationToken = default) => await RequestAsync<Address>(Method.Post, "addresses/create_and_verify", cancellationToken, parameters, "address");

        /// <summary>
        ///     Create and verify an <see cref="Address"/> in one API call.
        ///     <a href="https://www.easypost.com/docs/api#create-and-verify-addresses">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Address"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Address"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Address> CreateAndVerify(Parameters.Address.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Address>(Method.Post, "addresses/create_and_verify", cancellationToken, parameters.ToDictionary(), "address");
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
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An EasyPost.AddressCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<AddressCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            AddressCollection collection = await RequestAsync<AddressCollection>(Method.Get, "addresses", cancellationToken, parameters);
            collection.Filters = Parameters.Address.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Address"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.Address.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="AddressCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<AddressCollection> All(Parameters.Address.All parameters, CancellationToken cancellationToken = default)
        {
            AddressCollection collection = await RequestAsync<AddressCollection>(Method.Get, "addresses", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="AddressCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="AddressCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="AddressCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<AddressCollection> GetNextPage(AddressCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<AddressCollection, Parameters.Address.All>(async parameters => await All(parameters, cancellationToken), collection.Addresses, pageSize);

        /// <summary>
        ///     Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Address instance.</returns>
        [CrudOperations.Read]
        public async Task<Address> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Address>(Method.Get, $"addresses/{id}", cancellationToken);

        /// <summary>
        ///     Verify an Address.
        /// </summary>
        /// <param name="id">ID of the address to verify.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        [CrudOperations.Update]
        public async Task<Address> Verify(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Address>(Method.Get, $"addresses/{id}/verify", cancellationToken, rootElement: "address");
        }

        #endregion
    }
}
