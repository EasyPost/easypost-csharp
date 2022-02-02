using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace EasyPost
{
    public class Batch : Resource
    {
        public DateTime? created_at { get; set; }
        public string error { get; set; }
        public string id { get; set; }
        public string label_url { get; set; }
        public string message { get; set; }
        public string mode { get; set; }
        public int num_shipments { get; set; }
        public string reference { get; set; }
        public ScanForm scan_form { get; set; }
        public List<BatchShipment> shipments { get; set; }
        public string state { get; set; }
        public Dictionary<string, int> status { get; set; }
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        public void AddShipments(IEnumerable<string> shipmentIds)
        {
            Request request = new Request("batches/{id}/add_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> lShipments = shipmentIds
                .Select(shipmentId => new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "shipments", lShipments
                }
            });

            Merge(request.Execute<Batch>());
        }

        /// <summary>
        ///     Add shipments to this batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be added.</param>
        public void AddShipments(IEnumerable<Shipment> shipments) => AddShipments(shipments.Select(shipment => shipment.id).ToList());

        /// <summary>
        ///     Purchase all shipments within this batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        public void Buy()
        {
            Request request = new Request("batches/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Batch>());
        }

        /// <summary>
        ///     Asynchronously generate a label containing all of the Shipment labels belonging to this batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        public void GenerateLabel(string fileFormat)
        {
            Request request = new Request("batches/{id}/label", Method.POST);
            request.AddUrlSegment("id", id);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "file_format", fileFormat
                }
            };

            request.AddQueryString(parameters);
            Merge(request.Execute<Batch>());
        }

        /// <summary>
        ///     Asynchronously generate a scan from for this batch.
        /// </summary>
        public void GenerateScanForm()
        {
            Request request = new Request("batches/{id}/scan_form", Method.POST);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Batch>());
        }

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        public void RemoveShipments(IEnumerable<string> shipmentIds)
        {
            Request request = new Request("batches/{id}/remove_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            List<Dictionary<string, object>> lShipments = shipmentIds
                .Select(shipmentId => new Dictionary<string, object>
                {
                    {
                        "id", shipmentId
                    }
                }).ToList();
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "shipments", lShipments
                }
            });

            Merge(request.Execute<Batch>());
        }

        /// <summary>
        ///     Remove shipments from this batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be removed.</param>
        public void RemoveShipments(IEnumerable<Shipment> shipments) => RemoveShipments(shipments.Select(shipment => shipment.id).ToList());

        /// <summary>
        ///     Create a Batch.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///     * {"shipments", List&lt;Dictionary&lt;string, object&gt;&gt;} See Shipment.Create for a list of valid keys.
        ///     * {"reference", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static Batch Create(Dictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("batches", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "batch", parameters
                }
            });

            return request.Execute<Batch>();
        }


        /// <summary>
        ///     Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static Batch Retrieve(string id)
        {
            Request request = new Request("batches/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Batch>();
        }

        /// <summary>
        ///     List all Batch objects.
        /// </summary>
        /// <param name="parameters">Optional dictionary containing parameters for request.</param>
        /// <returns>EasyPost.BatchCollection instance.</returns>
        public static BatchCollection All(Dictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("batches");
            request.AddQueryString(parameters);

            return request.Execute<BatchCollection>();
        }
    }
}
