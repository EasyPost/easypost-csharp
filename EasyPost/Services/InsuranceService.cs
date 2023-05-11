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
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#insurance">insurance-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InsuranceService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InsuranceService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal InsuranceService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an <see cref="Insurance"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-insurance">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Insurance"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Insurance"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Insurance> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("insurance");
            return await RequestAsync<Insurance>(Method.Post, "insurances", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create an <see cref="Insurance"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-insurance">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Insurance"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Insurance"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Insurance> Create(Parameters.Insurance.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Insurance>(Method.Post, "insurances", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="Insurance"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-insurances">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Insurance"/>s.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="InsuranceCollection"/> instance containing <see cref="Insurance"/>s instances.</returns>
        [CrudOperations.Read]
        public async Task<InsuranceCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            InsuranceCollection collection = await RequestAsync<InsuranceCollection>(Method.Get, "insurances", cancellationToken, parameters);
            collection.Filters = Parameters.Insurance.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Insurance"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-insurances">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Insurance"/>s.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="InsuranceCollection"/> instance containing <see cref="Insurance"/>s instances.</returns>
        [CrudOperations.Read]
        public async Task<InsuranceCollection> All(Parameters.Insurance.All parameters, CancellationToken cancellationToken = default)
        {
            InsuranceCollection collection = await RequestAsync<InsuranceCollection>(Method.Get, "insurances", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="InsuranceCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-insurances">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="InsuranceCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="InsuranceCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<InsuranceCollection> GetNextPage(InsuranceCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<InsuranceCollection, Parameters.Insurance.All>(async parameters => await All(parameters, cancellationToken), collection.Insurances, pageSize);

        /// <summary>
        ///     Retrieve an <see cref="Insurance"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-an-insurance">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Insurance"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Insurance"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<Insurance> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Insurance>(Method.Get, $"insurances/{id}", cancellationToken);

        #endregion
    }
}
