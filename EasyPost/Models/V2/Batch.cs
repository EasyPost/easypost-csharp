using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Batch : EasyPostObject
    {
        [JsonProperty("error")]
        public string? Error { get; set; }
        [JsonProperty("label_url")]
        public string? LabelUrl { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("num_shipments")]
        public int NumShipments { get; set; }
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

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> AddShipments(IEnumerable<string?> shipmentIds)
        {
            List<Dictionary<string, object>> realShipmentIds = (from shipmentId in shipmentIds
                where shipmentId != null
                select new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            return await Update<Batch>(Method.Post, $"batches/{Id}/add_shipments", new Dictionary<string, object>
            {
                {
                    "shipments", realShipmentIds
                }
            });
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentsToAdd">List of Shipment objects to be added.</param>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> AddShipments(IEnumerable<Shipment> shipmentsToAdd) => await AddShipments(shipmentsToAdd.Select(shipment => shipment.Id).ToList());

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> Buy()
        {
            return await Update<Batch>(Method.Post, $"batches/{Id}/buy");
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> GenerateLabel(string fileFormat)
        {
            return await Update<Batch>(Method.Post, $"batches/{Id}/label", new Dictionary<string, object>
            {
                {
                    "file_format", fileFormat
                }
            });
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> GenerateScanForm()
        {
            return await Update<Batch>(Method.Post, $"batches/{Id}/scan_form");
        }

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> RemoveShipments(IEnumerable<string?> shipmentIds)
        {
            List<Dictionary<string, object>> realShipmentIds = (from shipmentId in shipmentIds
                where shipmentId != null
                select new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            return await Update<Batch>(Method.Post, $"batches/{Id}/remove_shipments", new Dictionary<string, object>
            {
                {
                    "shipments", realShipmentIds
                }
            });
        }

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipmentsToRemove">List of Shipment objects to be removed.</param>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Batch> RemoveShipments(IEnumerable<Shipment> shipmentsToRemove) => await RemoveShipments(shipmentsToRemove.Select(shipment => shipment.Id).ToList());
    }
}
