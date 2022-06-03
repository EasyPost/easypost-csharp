using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Refund : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }

        [JsonProperty("confirmation_number")]
        public string confirmation_number { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("carrier")]
        public string carrier { get; set; }

        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }

        /// <summary>
        ///     Create a Refund.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the refund with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of EasyPost.Refund instances.</returns>
        public static async Task<List<Refund>> Create(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("refunds", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "refund", parameters
                }
            });

            return await request.Execute<List<Refund>>();
        }

        /// <summary>
        ///     Retrieve a Refund from its id.
        /// </summary>
        /// <param name="id">String representing a Refund. Starts with "rfnd_".</param>
        /// <returns>EasyPost.Refund instance.</returns>
        public static async Task<Refund> Retrieve(string id)
        {
            Request request = new Request("refunds/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Refund>();
        }

        /// <summary>
        ///     List all Refund objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.RefundCollection instance.</returns>
        public static async Task<RefundCollection> All(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("refunds", Method.Get);
            request.AddParameters(parameters);

            return await request.Execute<RefundCollection>();
        }
    }
}
