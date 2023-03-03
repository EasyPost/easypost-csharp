using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Annotations;
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
        ///     Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Read]
        public async Task<Batch> Retrieve(string id) => await Get<Batch>($"batches/{id}");

        #endregion
    }
}
