using EasyPost;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Batch {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string state { get; set; }
        public string num_shipments { get; set; }
        public string reference { get; set; }
        public List<BatchShipment> shipments { get; set; }
        public Dictionary<string, int> status { get; set; }
        public ScanForm scan_form { get; set; }
        public string mode { get; set; }
        public string error { get; set; }
        public string message { get; set; }

        private static Client client = new Client();

        /// <summary>
        /// Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static Batch Retrieve(string id) {
            Request request = new Request("batches/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<Batch>(request);
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
            request.addBody(parameters, "batch");

            return client.Execute<Batch>(request);
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        public void AddShipments(List<string> shipmentIds) {
            Request request = new Request("batches/{id}/add_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> body = shipmentIds.Select(shipmentId => new Dictionary<string, object>() {{"id", shipmentId}}).ToList();
            request.addBody(body, "shipments");

            Resource.Merge(this, client.Execute<Batch>(request));
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be added.</param>
        public void AddShipments(List<Shipment> shipments) {
            AddShipments(shipments.Select(shipment => shipment.id).ToList());
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        public void RemoveShipments(List<string> shipmentIds) {
            Request request = new Request("batches/{id}/remove_shipments", Method.POST);
            request.AddUrlSegment("id", id);
            request.addBody(shipmentIds, "shipments");

            Resource.Merge(this, client.Execute<Batch>(request));
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be removed.</param>
        public void RemoveShipments(List<Shipment> shipments) {
            RemoveShipments(shipments.Select(shipment => shipment.id).ToList());
        }

        /// <summary>
        /// Purchase all shipments within a batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        public void Buy() {
            Request request = new Request("batches/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);

            Resource.Merge(this, client.Execute<Batch>(request));
        }

        /// <summary>
        /// Asynchronously generate a label containing all of the Shimpent labels belonging to the batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="orderBy">Optional parameter to order the generated label. Ex: "reference DESC"</param>
        public void GenerateLabel(string fileFormat, string orderBy = null) {
            Request request = new Request("batches/{id}/label", Method.POST);
            request.AddUrlSegment("id", id);
            request.addBody(new List<Tuple<string, string>>() {
                new Tuple<string, string>("file_format", fileFormat),
                new Tuple<string, string>("order_by", orderBy)
            });

            Resource.Merge(this, client.Execute<Batch>(request));
        }

        /// <summary>
        /// Asychronously generate a scan from for the batch.
        /// </summary>
        public void GenerateScanForm() {
            Request request = new Request("batches/{id}/scan_form", Method.POST);
            request.AddUrlSegment("id", id);

            Resource.Merge(this, client.Execute<Batch>(request));
        }
    }
}
