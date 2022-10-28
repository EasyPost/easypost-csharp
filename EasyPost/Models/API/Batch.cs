using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Batch : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("error")]
        public string? Error { get; set; }
        [JsonProperty("label_url")]
        public string? LabelUrl { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("num_shipments")]
        public int? NumShipments { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("scan_form")]
        public ScanForm? ScanForm { get; set; }
        [JsonProperty("shipments")]
        public List<BatchShipment>? Shipments { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("status")]
        public Dictionary<string, int>? Status { get; set; }

        #endregion

        internal Batch()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="parameters">UpdateShipmentParameters</param>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(Dictionary<string, object> parameters)
        {
            // parameters = parameters.Wrap("batch");  // TODO: Update docs to remove wrapped "batch" key
            await Update<Batch>(Method.Post, $"batches/{Id}/add_shipments", parameters);
            return this;
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be added.</param>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(List<Shipment> shipmentsToAdd)
        {
            var parameters = new Dictionary<string, object> { { "shipments", shipmentsToAdd } };
            return await AddShipments(parameters);
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(IEnumerable<string> shipmentIds)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await AddShipments(shipments);
        }

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        [CrudOperations.Update]
        public async Task<Batch> Buy()
        {
            await Update<Batch>(Method.Post, $"batches/{Id}/buy");
            return this;
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string fileFormat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "file_format", fileFormat } };
            await Update<Batch>(Method.Post, $"batches/{Id}/label", parameters);
            return this;
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string fileFormat = "pdf")
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "file_format", fileFormat } };
            await Update<Batch>(Method.Post, $"batches/{Id}/scan_form", parameters);
            return this;
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="parameters">UpdateShipmentParameters</param>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(Dictionary<string, object> parameters)
        {
            // parameters = parameters.Wrap("batch");  // TODO: Update docs to remove wrapped "batch" key
            await Update<Batch>(Method.Post, $"batches/{Id}/remove_shipments", parameters);
            return this;
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be removed.</param>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(List<Shipment> shipmentsToAdd)
        {
            var parameters = new Dictionary<string, object> { { "shipments", shipmentsToAdd } };
            return await RemoveShipments(parameters);
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(IEnumerable<string> shipmentIds)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment { Id = shipmentId }).ToList();
            return await RemoveShipments(shipments);
        }

        #endregion
    }

    public class BatchCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("batches")]
        public List<Batch>? Batches { get; set; }

        #endregion

        internal BatchCollection()
        {
        }
    }
}
