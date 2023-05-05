using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#refunds">refund-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RefundService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefundService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal RefundService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Refund.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the refund with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of EasyPost.Refund instances.</returns>
        [CrudOperations.Create]
        public async Task<List<Refund>> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("refund");
            return await RequestAsync<List<Refund>>(Method.Post, "refunds", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Refund"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Refunds.Create"/> parameter set.</param>
        /// <returns><see cref="Refund"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<List<Refund>> Create(BetaFeatures.Parameters.Refunds.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<List<Refund>>(Method.Post, "refunds", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all Refund objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.RefundCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<RefundCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            RefundCollection collection = await RequestAsync<RefundCollection>(Method.Get, "refunds", cancellationToken, parameters);
            collection.Filters = BetaFeatures.Parameters.Refunds.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Refund"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Refunds.All"/> parameter set.</param>
        /// <returns><see cref="RefundCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<RefundCollection> All(BetaFeatures.Parameters.Refunds.All parameters, CancellationToken cancellationToken = default)
        {
            RefundCollection collection = await RequestAsync<RefundCollection>(Method.Get, "refunds", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="RefundCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="RefundCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="RefundCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<RefundCollection> GetNextPage(RefundCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<RefundCollection, BetaFeatures.Parameters.Refunds.All>(async parameters => await All(parameters, cancellationToken), collection.Refunds, pageSize);

        /// <summary>
        ///     Retrieve a Refund from its id.
        /// </summary>
        /// <param name="id">String representing a Refund. Starts with "rfnd_".</param>
        /// <returns>EasyPost.Refund instance.</returns>
        [CrudOperations.Read]
        public async Task<Refund> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Refund>(Method.Get, $"refunds/{id}", cancellationToken);

        #endregion
    }
}
