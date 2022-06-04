using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Batch : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("error")]
        public string? error { get; set; }
        [JsonProperty("id")]
        public string? id { get; set; }
        [JsonProperty("label_url")]
        public string? label_url { get; set; }
        [JsonProperty("message")]
        public string? message { get; set; }
        [JsonProperty("mode")]
        public string? mode { get; set; }
        [JsonProperty("num_shipments")]
        public int num_shipments { get; set; }
        [JsonProperty("reference")]
        public string? reference { get; set; }
        [JsonProperty("scan_form")]
        public ScanForm? scan_form { get; set; }
        [JsonProperty("shipments")]
        public List<BatchShipment>? shipments { get; set; }
        [JsonProperty("state")]
        public string? state { get; set; }
        [JsonProperty("status")]
        public Dictionary<string, int>? status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        public async Task AddShipments(IEnumerable<string?> shipmentIds)
        {
            List<Dictionary<string, object>> realShipmentIds = (from shipmentId in shipmentIds
                                                                where shipmentId != null
                                                                select new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            await Update<Batch>(Method.Post, $"batches/{id}/add_shipments", new Dictionary<string, object>
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
        public async Task AddShipments(IEnumerable<Shipment> shipmentsToAdd) => await AddShipments(shipmentsToAdd.Select(shipment => shipment.id).ToList());

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        public async Task Buy() => await Update<Batch>(Method.Post, $"batches/{id}/buy");

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        public async Task GenerateLabel(string fileFormat) =>
            await Update<Batch>(Method.Post, $"batches/{id}/label", new Dictionary<string, object>
            {
                {
                    "file_format", fileFormat
                }
            });

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        public async Task GenerateScanForm() => await Update<Batch>(Method.Post, $"batches/{id}/scan_form");

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        public async Task RemoveShipments(IEnumerable<string?> shipmentIds)
        {
            List<Dictionary<string, object>> realShipmentIds = (from shipmentId in shipmentIds
                                                                where shipmentId != null
                                                                select new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            await Update<Batch>(Method.Post, $"batches/{id}/remove_shipments", new Dictionary<string, object>
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
        public async Task RemoveShipments(IEnumerable<Shipment> shipmentsToRemove) => await RemoveShipments(shipmentsToRemove.Select(shipment => shipment.id).ToList());
    }
}
