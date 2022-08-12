using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Batch : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("label_url")]
        public string label_url { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("mode")]
        public new string mode { get; set; }
        [JsonProperty("num_shipments")]
        public int num_shipments { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("scan_form")]
        public ScanForm scan_form { get; set; }
        [JsonProperty("shipments")]
        public List<BatchShipment> shipments { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("status")]
        public Dictionary<string, int> status { get; set; }

        #endregion

        #region CRUD Operations

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="parameters">UpdateShipmentParameters</param>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(Dictionary<string, object?> parameters) => await Update<Batch>(Method.Post, $"batches/{id}/add_shipments", parameters);

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be added.</param>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(List<Shipment> shipmentsToAdd)
        {
            var parameters = new Dictionary<string, object?>
            {
                {
                    "shipments", shipmentsToAdd
                }
            };
            return await AddShipments(parameters);
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        [CrudOperations.Update]
        public async Task<Batch> AddShipments(IEnumerable<string> shipmentIds)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment
            {
                id = shipmentId
            })
                .ToList();

            return await AddShipments(shipments);
        }

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        [CrudOperations.Update]
        public async Task<Batch> Buy()
        {
            return await Update<Batch>(Method.Post, $"batches/{id}/buy");
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [CrudOperations.Update]
        public async Task<Batch> GenerateLabel(string fileFormat = "pdf")
        {
            Dictionary<string, object?> parameters = new Dictionary<string, object?>
            {
                {
                    "file_format", fileFormat
                }
            };
            return await Update<Batch>(Method.Post, $"batches/{id}/label", parameters);
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [CrudOperations.Update]
        public async Task<Batch> GenerateScanForm(string fileFormat = "pdf")
        {
            Dictionary<string, object?> parameters = new Dictionary<string, object?>
            {
                {
                    "file_format", fileFormat
                }
            };
            return await Update<Batch>(Method.Post, $"batches/{id}/scan_form", parameters);
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="parameters">UpdateShipmentParameters</param>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(Dictionary<string, object?> parameters) => await Update<Batch>(Method.Post, $"batches/{id}/remove_shipments", parameters);

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be removed.</param>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(List<Shipment> shipmentsToAdd)
        {
            var parameters = new Dictionary<string, object?>
            {
                {
                    "shipments", shipmentsToAdd
                }
            };
            return await RemoveShipments(parameters);
        }

        /// <summary>
        ///     Remove shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        [CrudOperations.Update]
        public async Task<Batch> RemoveShipments(IEnumerable<string> shipmentIds)
        {
            List<Shipment> shipments = shipmentIds.Select(shipmentId => new Shipment
            {
                id = shipmentId
            })
                .ToList();

            return await RemoveShipments(shipments);
        }

        #endregion
    }
}
