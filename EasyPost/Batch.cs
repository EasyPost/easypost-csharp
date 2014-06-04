using EasyPost;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Batch {
        public class BatchShipment {
            public string id { get; set; }
            public string batch_status { get; set; }
            public string batch_message { get; set; }
        }

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
        ///   * {"shipments", List<Dictionary<string, object>>} See Shipment.Create for a list of valid Shipment keys.
        ///   * {"reference", string} Identifier for batch.
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
        /// 
        /// </summary>
        /// <param name="shipmentIds"></param>
        /// <returns></returns>
        public void AddShipments(List<string> shipmentIds) {
            Request request = new Request("batchs/{id}/add_shipments");
            request.AddUrlSegment("id", id);
            request.addBody(shipmentIds, "shipments");

            Resource.Merge(this, client.Execute<Batch>(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipmentIds"></param>
        /// <returns></returns>
        public void RemoveShipments(List<string> shipmentIds) {
            Request request = new Request("batchs/{id}/remove_shipments");
            request.AddUrlSegment("id", id);
            request.addBody(shipmentIds, "shipments");

            Resource.Merge(this, client.Execute<Batch>(request));
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
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2"</param>
        /// <param name="orderBy">Optional parameter to order the generated label. Ex: "reference DESC"</param>
        public void GenerateLabel(string fileFormat, string orderBy = null) {
            string body = String.Join("&", new List<string>() {
                string.Concat(Uri.EscapeDataString("file_format"), "=", Uri.EscapeDataString(fileFormat)),
                string.Concat(Uri.EscapeDataString("order_by"), "=", Uri.EscapeDataString(orderBy))
            });

            Request request = new Request("batches/{id}/label", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

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
