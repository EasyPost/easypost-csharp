using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Tracker : Resource
    {
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail carrier_detail { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("est_delivery_date")]
        public DateTime? est_delivery_date { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("public_url")]
        public string public_url { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("signed_by")]
        public string signed_by { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail> tracking_details { get; set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime tracking_updated_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("weight")]
        public double? weight { get; set; }

        /// <summary>
        ///     Create a tracker.
        /// </summary>
        /// <param name="carrier">Carrier for the tracker.</param>
        /// <param name="trackingCode">Tracking code for the tracker.</param>
        /// <returns>An EasyPost.Tracker instance.</returns>
        public static Tracker Create(string carrier, string trackingCode)
        {
            Request request = new Request("trackers", Method.POST);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "tracking_code", trackingCode
                },
                {
                    "carrier", carrier
                }
            };

            request.AddBody(new Dictionary<string, object>
            {
                {
                    "tracker", parameters
                }
            });

            return request.Execute<Tracker>();
        }


        /// <summary>
        ///     Get a paginated list of trackers.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"tracking_code", string} Tracking number string. Only retrieve trackers with the given tracking code.
        ///     * {"carrier", string} String representing the tracker's carrier. Only retrieve trackers with the given carrier.
        ///     * {"before_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created before
        ///     this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created after this
        ///     id.
        ///     * {"start_datetime", datetime} Datetime representing the earliest possible tracker. Only retrieve trackers created
        ///     at or after this datetime. Defaults to 1 month ago.
        ///     * {"end_datetime", datetime} Datetime representing the latest possible tracker. Only retrieve trackers created
        ///     before this datetime. Defaults to the end of the current day.
        ///     * {"page_size", int} Size of page. Default to 30.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>Instance of EasyPost.ShipmentList.</returns>
        public static TrackerList All(Dictionary<string, object> parameters = null)
        {
            Request request = new Request("trackers");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            TrackerList trackerList = request.Execute<TrackerList>();
            trackerList.filters = parameters;
            return trackerList;
        }

        /// <summary>
        ///     Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <returns>EasyPost.Tracker instance.</returns>
        public static Tracker Retrieve(string id)
        {
            Request request = new Request("trackers/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Tracker>();
        }

        /// <summary>
        ///     Create a list of trackers
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers</param>
        /// <returns>True</returns>
        public static bool CreateList(Dictionary<string, object> parameters)
        {
            Request request = new Request("trackers/create_list", RestSharp.Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "trackers", parameters
                }
            });
            request.Execute<Tracker>();
            // This endpoint does not return a response so we return true here
            return true;
        }
    }
}
