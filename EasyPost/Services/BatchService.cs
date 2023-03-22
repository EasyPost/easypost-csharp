using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BatchService : EasyPostService
    {
        internal BatchService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Batch.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing optional parameters to create the batch with. Valid pairs:
        ///     * {"shipments", List&lt;Dictionary&lt;string, object&gt;&gt;} See Shipment.Create for a list of valid keys.
        ///     * {"reference", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> Create(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters?.Wrap("batch");
            return await Create<Batch>("batches", parameters);
        }

        /// <summary>
        ///     Create a <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.Create"/> parameter set.</param>
        /// <returns><see cref="Batch"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> Create(BetaFeatures.Parameters.Batches.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<Batch>("batches", parameters.ToDictionary());
        }

        /// <summary>
        ///     Create and buy a Batch in one step.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing optional parameters to create the batch with. Valid pairs:
        ///     * {"shipments", List&lt;Dictionary&lt;string, object&gt;&gt;} See Shipment.Create for a list of valid keys.
        ///     * {"reference", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> CreateAndBuy(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("batch");
            return await Create<Batch>("batches/create_and_buy", parameters);
        }

        /// <summary>
        ///     Create and buy a <see cref="Batch"/> in one step.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.Create"/> parameter set.</param>
        /// <returns><see cref="Batch"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> CreateAndBuy(BetaFeatures.Parameters.Batches.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<Batch>("batches/create_and_buy", parameters.ToDictionary());
        }

        /// <summary>
        ///     List all Batch objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a Batch ID. Starts with "batch_". Only retrieve batches created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a Batch ID. Starts with "batch_". Only retrieve batches created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve batches created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve batches created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.BatchCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<BatchCollection> All(Dictionary<string, object>? parameters = null) => await List<BatchCollection>("batches", parameters);

        /// <summary>
        ///     List all <see cref="Batch"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.All"/> parameter set.</param>
        /// <returns><see cref="BatchCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<BatchCollection> All(BetaFeatures.Parameters.Batches.All parameters) => await List<BatchCollection>("batches", parameters.ToDictionary());

        /// <summary>
        ///     Get the next page of a paginated <see cref="BatchCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="BatchCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="BatchCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<BatchCollection> GetNextPage(BatchCollection collection, int? pageSize = null) => await collection.GetNextPage<BatchCollection, BetaFeatures.Parameters.Batches.All>(async parameters => await All(parameters), collection.Batches, pageSize);

        /// <summary>
        ///     Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Read]
        public async Task<Batch> Retrieve(string id) => await Get<Batch>($"batches/{id}");

        #endregion
    }
}
