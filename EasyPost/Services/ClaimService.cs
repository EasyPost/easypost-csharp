using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#claims">claim-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ClaimService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClaimService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal ClaimService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an <see cref="Claim"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-insurance">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Claim"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Claim"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Claim> Create(Parameters.Claim.Create parameters, CancellationToken cancellationToken = default) => await RequestAsync<Claim>(Method.Post, "claims", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);

        /// <summary>
        ///     List all <see cref="Claim"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-insurances">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Claim"/>s.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ClaimCollection"/> instance containing <see cref="Claim"/>s instances.</returns>
        [CrudOperations.Read]
        public async Task<ClaimCollection> All(Parameters.Claim.All parameters, CancellationToken cancellationToken = default)
        {
            ClaimCollection collection = await RequestAsync<ClaimCollection>(Method.Get, "claims", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ClaimCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-insurances">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="ClaimCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="ClaimCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ClaimCollection> GetNextPage(ClaimCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ClaimCollection, Parameters.Claim.All>(async parameters => await All(parameters, cancellationToken), collection.Claims, pageSize);

        /// <summary>
        ///     Retrieve an <see cref="Claim"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-an-insurance">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Claim"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Claim"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<Claim> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Claim>(Method.Get, $"claims/{id}", cancellationToken, overrideApiVersion: ApiVersion.Beta);

        /// <summary>
        ///     Refund an <see cref="Claim"/>.
        ///     <a href="https://www.easypost.com/docs/api#refund-an-insurance">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Claim"/> to refund.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Claim"/> instance.</returns>
        [CrudOperations.Delete]
        public async Task<Claim> Cancel(string id, CancellationToken cancellationToken = default) => await RequestAsync<Claim>(Method.Post, $"claims/{id}/cancel", cancellationToken, overrideApiVersion: ApiVersion.Beta);

        #endregion
    }
}
