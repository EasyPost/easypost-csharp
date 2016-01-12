using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost {
    public class Batch : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public string state { get; set; }
        public int num_shipments { get; set; }
        public string reference { get; set; }
        public List<Shipment> shipments { get; set; }
        public Dictionary<string, int> status { get; set; }
        public ScanForm scan_form { get; set; }
        public string label_url { get; set; }
        public string mode { get; set; }
        public string error { get; set; }
        public string message { get; set; }

        /// <summary>
        /// Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static Batch Retrieve(string id) {
            Request request = new Request("batches/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Batch>();
        }

        /// <summary>
        /// Create a Batch.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"shipments", List<Dictionary<string, object>>} See Shipment.Create for a list of valid keys.
        ///   * {"reference", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static Batch Create(Dictionary<string, object> parameters = null) {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("batches", Method.POST);
            request.AddBody(parameters, "batch");

            return request.Execute<Batch>();
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        public void AddShipments(IEnumerable<string> shipmentIds) {
            Request request = new Request("batches/{id}/add_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> body = shipmentIds.Select(shipmentId => new Dictionary<string, object>() { { "id", shipmentId } }).ToList();
            request.AddBody(body, "shipments");

            this.Merge(request.Execute<Batch>());
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be added.</param>
        public void AddShipments(IEnumerable<Shipment> shipments) {
            AddShipments(shipments.Select(shipment => shipment.id).ToList());
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        public void RemoveShipments(IEnumerable<string> shipmentIds) {
            Request request = new Request("batches/{id}/remove_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> body = shipmentIds.Select(shipmentId => new Dictionary<string, object>() { { "id", shipmentId } }).ToList();
            request.AddBody(body, "shipments");

            this.Merge(request.Execute<Batch>());
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be removed.</param>
        public void RemoveShipments(IEnumerable<Shipment> shipments) {
            RemoveShipments(shipments.Select(shipment => shipment.id).ToList());
        }

        /// <summary>
        /// Purchase all shipments within a batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        public void Buy() {
            Request request = new Request("batches/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);

            this.Merge(request.Execute<Batch>());
        }

        /// <summary>
        /// Asynchronously generate a label containing all of the Shimpent labels belonging to the batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="orderBy">Optional parameter to order the generated label. Ex: "reference DESC"</param>
        public void GenerateLabel(string fileFormat, string orderBy = null) {
            Request request = new Request("batches/{id}/label", Method.POST);
            request.AddUrlSegment("id", id);

            List<Tuple<string, string>> body = new List<Tuple<string, string>>() {
                new Tuple<string, string>("file_format", fileFormat)
            };

            if (orderBy != null)
                body.Add(new Tuple<string, string>("order_by", orderBy));

            request.AddBody(body);
            this.Merge(request.Execute<Batch>());
        }

        /// <summary>
        /// Asychronously generate a scan from for the batch.
        /// </summary>
        public void GenerateScanForm() {
            Request request = new Request("batches/{id}/scan_form", Method.POST);
            request.AddUrlSegment("id", id);

            this.Merge(request.Execute<Batch>());
        }
    }
}
