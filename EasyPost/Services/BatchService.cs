using System.Collections.Generic;
using System.Linq;
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
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#batches">batch-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BatchService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BatchService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
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
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> Create(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            parameters = parameters?.Wrap("batch");
            return await RequestAsync<Batch>(Method.Post, "batches", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.Create"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="Batch"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> Create(BetaFeatures.Parameters.Batches.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Batch>(Method.Post, "batches", cancellationToken, parameters.ToDictionary());
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
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> CreateAndBuy(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("batch");
            return await RequestAsync<Batch>(Method.Post, "batches/create_and_buy", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create and buy a <see cref="Batch"/> in one step.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.Create"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="Batch"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Batch> CreateAndBuy(BetaFeatures.Parameters.Batches.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Batch>(Method.Post, "batches/create_and_buy", cancellationToken, parameters.ToDictionary());
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
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An EasyPost.BatchCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<BatchCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            BatchCollection collection = await RequestAsync<BatchCollection>(Method.Get, "batches", cancellationToken, parameters);
            collection.Filters = BetaFeatures.Parameters.Batches.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Batch"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="BatchCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<BatchCollection> All(BetaFeatures.Parameters.Batches.All parameters, CancellationToken cancellationToken = default)
        {
            BatchCollection collection = await RequestAsync<BatchCollection>(Method.Get, "batches", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        // TODO: Add GetNextPage function when Batches are sorted newest to oldest.

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="parameters">Update shipment parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/add_shipments", cancellationToken, parameters);
        }

        /// <summary>
        ///     Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Read]
        public async Task<Batch> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Batch>(Method.Get, $"batches/{id}", cancellationToken);

        /// <summary>
        ///     Add <see cref="Shipment"/>s to this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.AddShipments"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, BetaFeatures.Parameters.Batches.AddShipments parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/add_shipments", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be added.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, List<Shipment> shipmentsToAdd, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipmentsToAdd } };
            return await AddShipments(id, parameters, cancellationToken);
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, IEnumerable<string> shipmentIds, CancellationToken cancellationToken = default)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await AddShipments(id, shipments, cancellationToken);
        }

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> Buy(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/buy", cancellationToken);
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string id, string fileFormat, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/label", cancellationToken, parameters);
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the <see cref="Shipment"/> labels belonging to this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.GenerateLabel"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string id, BetaFeatures.Parameters.Batches.GenerateLabel parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/label", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string id, string fileFormat, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/scan_form", cancellationToken, parameters);
        }

        /// <summary>
        ///     Asynchronously generate a <see cref="ScanForm"/> for this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.GenerateScanForm"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string id, BetaFeatures.Parameters.Batches.GenerateScanForm parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/scan_form", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="parameters">Update shipment parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/remove_shipments", cancellationToken, parameters);
        }

        /// <summary>
        ///     Remove <see cref="Shipment"/>s from this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.RemoveShipments"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, BetaFeatures.Parameters.Batches.RemoveShipments parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/remove_shipments", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToRemove">List of Shipment objects to be removed.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, List<Shipment> shipmentsToRemove, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipmentsToRemove } };
            return await RemoveShipments(id, parameters, cancellationToken);
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, IEnumerable<string> shipmentIds, CancellationToken cancellationToken = default)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await RemoveShipments(id, shipments, cancellationToken);
        }

        #endregion
    }
}
