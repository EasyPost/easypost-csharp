using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#carrier-accounts">carrier account-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierAccountService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CarrierAccountService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal CarrierAccountService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        // TODO: Use ID or whole object as parameter?

        /// <summary>
        ///     Create a <see cref="CarrierAccount"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-carrier-account">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="CarrierAccount"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CarrierAccount"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CarrierAccount> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            if (parameters["type"] is not string carrierType)
            {
                throw new MissingParameterError("CarrierAccount type is required.");
            }

            string endpoint = Constants.CarrierAccounts.StandardCreateEndpoint;
            if (Constants.CarrierAccounts.IsCustomWorkflowType(carrierType))
            {
                endpoint = Constants.CarrierAccounts.CustomCreateEndpoint;
            }

            parameters = parameters.Wrap("carrier_account");

            return await RequestAsync<CarrierAccount>(Method.Post, endpoint, cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="CarrierAccount"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-carrier-account">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="CarrierAccount"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CarrierAccount"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CarrierAccount> Create(Parameters.CarrierAccount.ACreate parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            if (parameters.Type == null)
            {
                throw new MissingParameterError(nameof(parameters.Type));
            }

            // stop the user from using a generic Create parameter set when creating a carrier account with a custom workflow (e.g. FedExAccount)
            if (parameters.GetType() == typeof(Parameters.CarrierAccount.Create) && Constants.CarrierAccounts.IsCustomWorkflowType(parameters.Type))
            {
                throw new InvalidParameterError("parameters", $"Use a {parameters.Type} custom workflow parameter set instead.");
            }

            string endpoint = parameters.Endpoint;

            return await RequestAsync<CarrierAccount>(Method.Post, endpoint, cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all available <see cref="CarrierAccount"/>s.
        ///     <a href="https://www.easypost.com/docs/api#list-all-carrier-accounts">Related API documentation</a>.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="CarrierAccount"/>s.</returns>
        [CrudOperations.Read]
        public async Task<List<CarrierAccount>> All(CancellationToken cancellationToken = default) => await RequestAsync<List<CarrierAccount>>(Method.Get, "carrier_accounts", cancellationToken);

        /// <summary>
        ///     Retrieve a <see cref="CarrierAccount"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-carrieraccount">Related API documentation</a>.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The retrieved <see cref="CarrierAccount"/>.</returns>
        [CrudOperations.Read]
        public async Task<CarrierAccount> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<CarrierAccount>(Method.Get, $"carrier_accounts/{id}", cancellationToken);

        /// <summary>
        ///     Update a <see cref="CarrierAccount"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-a-carrieraccount">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to update <see cref="CarrierAccount"/> with.</param>
        /// <param name="id">ID of the <see cref="CarrierAccount"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="CarrierAccount"/>.</returns>
        [CrudOperations.Update]
        public async Task<CarrierAccount> Update(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("carrier_account");
            return await RequestAsync<CarrierAccount>(Method.Put, $"carrier_accounts/{id}", cancellationToken, parameters);
        }

        /// <summary>
        ///     Update a <see cref="CarrierAccount"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-a-carrieraccount">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to update <see cref="CarrierAccount"/> with.</param>
        /// <param name="id">ID of the <see cref="CarrierAccount"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="CarrierAccount"/>.</returns>
        [CrudOperations.Update]
        public async Task<CarrierAccount> Update(string id, Parameters.CarrierAccount.Update parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<CarrierAccount>(Method.Put, $"carrier_accounts/{id}", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Delete a <see cref="CarrierAccount"/>.
        ///     <a href="https://www.easypost.com/docs/api#delete-a-carrier-account">Related API documentation</a>.
        /// </summary>
        /// <param name="id">ID of the <see cref="CarrierAccount"/> to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>None.</returns>
        [CrudOperations.Delete]
        public async Task Delete(string id, CancellationToken cancellationToken = default) => await RequestAsync(Method.Delete, $"carrier_accounts/{id}", cancellationToken);

        #endregion
    }
}
