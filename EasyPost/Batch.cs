using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Batch : Resource
    {
        #region JSON Properties

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

        #endregion

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
        public static async Task<BatchCollection> All(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("batches", Method.Get);
            request.AddParameters(parameters);

            return await request.Execute<BatchCollection>();
        }

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
        public static async Task<Batch> Create(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("batches", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "batch", parameters
                }
            });

            return await request.Execute<Batch>();
        }

        /// <summary>
        ///     Create and buy a Batch in one step.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///     * {"shipments", List&lt;Dictionary&lt;string, object&gt;&gt;} See Shipment.Create for a list of valid keys.
        ///     * {"reference", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static async Task<Batch> CreateAndBuy(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("batches/create_and_buy", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "batch", parameters
                }
            });

            return await request.Execute<Batch>();
        }

        /// <summary>
        ///     Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static async Task<Batch> Retrieve(string id)
        {
            Request request = new Request("batches/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Batch>();
        }
    }
}
