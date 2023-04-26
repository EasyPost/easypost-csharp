using System.Collections.Generic;
using System.Linq;
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

        // TODO: Add GetNextPage function when Batches are sorted newest to oldest.

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="parameters">Update shipment parameters.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, Dictionary<string, object> parameters)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/add_shipments", parameters);
        }

        /// <summary>
        ///     Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        [CrudOperations.Read]
        public async Task<Batch> Retrieve(string id) => await Request<Batch>(Method.Get, $"batches/{id}");

        /// <summary>
        ///     Add <see cref="Shipment"/>s to this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.AddShipments"/> parameter set.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, BetaFeatures.Parameters.Batches.AddShipments parameters)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/add_shipments", parameters.ToDictionary());
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be added.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, List<Shipment> shipmentsToAdd)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipmentsToAdd } };
            return await AddShipments(id, parameters);
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(string id, IEnumerable<string> shipmentIds)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await AddShipments(id, shipments);
        }

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> Buy(string id)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/buy");
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string id, string fileFormat = "pdf") // TODO: Remove default value (breaking change)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };
            return await Request<Batch>(Method.Post, $"batches/{id}/label", parameters);
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the <see cref="Shipment"/> labels belonging to this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.GenerateLabel"/> parameter set.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string id, BetaFeatures.Parameters.Batches.GenerateLabel parameters)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/label", parameters.ToDictionary());
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string id, string fileFormat = "pdf") // TODO: Remove default value (breaking change)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };
            return await Request<Batch>(Method.Post, $"batches/{id}/scan_form", parameters);
        }

        /// <summary>
        ///     Asynchronously generate a <see cref="ScanForm"/> for this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.GenerateScanForm"/> parameter set.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string id, BetaFeatures.Parameters.Batches.GenerateScanForm parameters)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/scan_form", parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="parameters">Update shipment parameters.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, Dictionary<string, object> parameters)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/remove_shipments", parameters);
        }

        /// <summary>
        ///     Remove <see cref="Shipment"/>s from this <see cref="Batch"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Batches.RemoveShipments"/> parameter set.</param>
        /// <returns>This updated <see cref="Batch"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, BetaFeatures.Parameters.Batches.RemoveShipments parameters)
        {
            return await Request<Batch>(Method.Post, $"batches/{id}/remove_shipments", parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToRemove">List of Shipment objects to be removed.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, List<Shipment> shipmentsToRemove)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipmentsToRemove } };
            return await RemoveShipments(id, parameters);
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        /// <returns>The updated Batch.</returns>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(string id, IEnumerable<string> shipmentIds)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await RemoveShipments(id, shipments);
        }

        #endregion
    }
}
