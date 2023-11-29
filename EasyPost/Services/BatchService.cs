using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
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
        ///     Create a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Batch"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Batch"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Batch> Create(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            parameters = parameters?.Wrap("batch");
            return await RequestAsync<Batch>(Method.Post, "batches", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Batch"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Batch"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Batch> Create(Parameters.Batch.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Batch>(Method.Post, "batches", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="Batch"/> objects.
        ///     <a href="https://www.easypost.com/docs/api#list-all-batches">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Dictionary containing parameters to filter the result list with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="BatchCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<BatchCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            BatchCollection collection = await RequestAsync<BatchCollection>(Method.Get, "batches", cancellationToken, parameters);
            collection.Filters = Parameters.Batch.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Batch"/> objects.
        ///     <a href="https://www.easypost.com/docs/api#list-all-batches">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.Batch.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="BatchCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<BatchCollection> All(Parameters.Batch.All parameters, CancellationToken cancellationToken = default)
        {
            BatchCollection collection = await RequestAsync<BatchCollection>(Method.Get, "batches", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        // TODO: Add GetNextPage function when Batches are sorted newest to oldest.

        /// <summary>
        ///     Add <see cref="Shipment"/>s to a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#add-shipments-to-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to add <see cref="Shipment"/>s to.</param>
        /// <param name="parameters">Parameters for the <see cref="Shipment"/>s to add.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/add_shipments", cancellationToken, parameters);
        }

        /// <summary>
        ///     Retrieve a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The requested <see cref="Batch"/>.</returns>
        [CrudOperations.Read]
        public async Task<Batch> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Batch>(Method.Get, $"batches/{id}", cancellationToken);

        /// <summary>
        ///     Add <see cref="Shipment"/>s to a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#add-shipments-to-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to add <see cref="Shipment"/>s to.</param>
        /// <param name="parameters">Parameters for the <see cref="Shipment"/>s to add.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, Parameters.Batch.AddShipments parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/add_shipments", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Add <see cref="Shipment"/>s to a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#add-shipments-to-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to add <see cref="Shipment"/>s to.</param>
        /// <param name="shipmentsToAdd">List of <see cref="Shipment"/>s to add to the <see cref="Batch"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, List<Shipment> shipmentsToAdd, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipmentsToAdd } };
            return await AddShipments(id, parameters, cancellationToken);
        }

        /// <summary>
        ///     Add <see cref="Shipment"/>s to a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#add-shipments-to-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to add <see cref="Shipment"/>s to.</param>
        /// <param name="shipmentIds">List of IDs of <see cref="Shipment"/>s to add to the <see cref="Batch"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, IEnumerable<string> shipmentIds, CancellationToken cancellationToken = default)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await AddShipments(id, shipments, cancellationToken);
        }

        /// <summary>
        ///     Purchase all <see cref="Shipment"/>s within a <see cref="Batch"/>. The <see cref="Batch.State"/> must be "created" before purchasing.
        ///     <a href="https://www.easypost.com/docs/api#buy-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to purchase <see cref="Shipment"/>s from.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> Buy(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/buy", cancellationToken);
        }

        /// <summary>
        ///     Asynchronously generate a <see cref="PostageLabel"/> containing all of the <see cref="Shipment"/> labels belonging to a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#batch-labels">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to generate a label for.</param>
        /// <param name="fileFormat">Format to generate the label in. Must be "pdf" or "zpl".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string id, string fileFormat, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/label", cancellationToken, parameters);
        }

        /// <summary>
        ///     Asynchronously generate a <see cref="PostageLabel"/> containing all of the <see cref="Shipment"/> labels belonging to this <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#batch-labels">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to generate a label for.</param>
        /// <param name="parameters"><see cref="Parameters.Batch.GenerateLabel"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string id, Parameters.Batch.GenerateLabel parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/label", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Asynchronously generate a <see cref="ScanForm"/> for a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#manifesting-scan-form">Related API documentation</a>.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string id, string fileFormat, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/scan_form", cancellationToken, parameters);
        }

        /// <summary>
        ///     Asynchronously generate a <see cref="ScanForm"/> for this <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#manifesting-scan-form">Related API documentation</a>.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <param name="parameters"><see cref="Parameters.Batch.GenerateScanForm"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string id, Parameters.Batch.GenerateScanForm parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/scan_form", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove <see cref="Shipment"/>s from a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#remove-shipments-from-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to remove <see cref="Shipment"/>s from.</param>
        /// <param name="parameters">Parameters for the <see cref="Shipment"/>s to remove.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/remove_shipments", cancellationToken, parameters);
        }

        /// <summary>
        ///     Remove <see cref="Shipment"/>s from a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#remove-shipments-from-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to remove <see cref="Shipment"/>s from.</param>
        /// <param name="parameters">Parameters for the <see cref="Shipment"/>s to remove.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, Parameters.Batch.RemoveShipments parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Batch>(Method.Post, $"batches/{id}/remove_shipments", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove <see cref="Shipment"/>s from a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#remove-shipments-from-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to remove <see cref="Shipment"/>s from.</param>
        /// <param name="shipmentsToRemove">List of <see cref="Shipment"/>s to remove from the <see cref="Batch"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, List<Shipment> shipmentsToRemove, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipmentsToRemove } };
            return await RemoveShipments(id, parameters, cancellationToken);
        }

        /// <summary>
        ///     Remove <see cref="Shipment"/>s from a <see cref="Batch"/>.
        ///     <a href="https://www.easypost.com/docs/api#remove-shipments-from-a-batch">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Batch"/> to remove <see cref="Shipment"/>s from.</param>
        /// <param name="shipmentIds">List of IDs of <see cref="Shipment"/>s to remove from the <see cref="Batch"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Batch"/> object.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, IEnumerable<string> shipmentIds, CancellationToken cancellationToken = default)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await RemoveShipments(id, shipments, cancellationToken);
        }

        #endregion
    }
}
