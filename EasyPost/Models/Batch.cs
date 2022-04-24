using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
{
    public class Batch : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("label_url")]
        public string label_url { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
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
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        public async Task AddShipments(IEnumerable<string> shipmentIds)
        {
            Request request = new Request("batches/{id}/add_shipments", Method.Post);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> lShipments = shipmentIds
                .Select(shipmentId => new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "shipments", lShipments
                }
            });

            Merge(await request.Execute<Batch>());
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be added.</param>
        public async Task AddShipments(IEnumerable<Shipment> shipments) => await AddShipments(shipments.Select(shipment => shipment.id).ToList());

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        public async Task Buy()
        {
            Request request = new Request("batches/{id}/buy", Method.Post);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Batch>());
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        public async Task GenerateLabel(string fileFormat)
        {
            Request request = new Request("batches/{id}/label", Method.Post);
            request.AddUrlSegment("id", id);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "file_format", fileFormat
                }
            };

            request.AddParameters(parameters);
            Merge(await request.Execute<Batch>());
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        public async Task GenerateScanForm()
        {
            Request request = new Request("batches/{id}/scan_form", Method.Post);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Batch>());
        }

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        public async Task RemoveShipments(IEnumerable<string> shipmentIds)
        {
            Request request = new Request("batches/{id}/remove_shipments", Method.Post);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> lShipments = shipmentIds
                .Select(shipmentId => new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "shipments", lShipments
                }
            });

            Merge(await request.Execute<Batch>());
        }

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be removed.</param>
        public async Task RemoveShipments(IEnumerable<Shipment> shipments) => await RemoveShipments(shipments.Select(shipment => shipment.id).ToList());
    }
}
