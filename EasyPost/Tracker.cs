using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Tracker : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime tracking_updated_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? est_delivery_date { get; set; }
        public string mode { get; set; }
        public string shipment_id { get; set; }
        public string status { get; set; }
        public string carrier { get; set; }
        public string tracking_code { get; set; }
        public string signed_by { get; set; }
        public double? weight { get; set; }
        public string public_url { get; set; }
        public List<TrackingDetail> tracking_details { get; set; }
        public CarrierDetail carrier_detail { get; set; }

        /// <summary>
        /// Get a paginated list of trackers.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"tracking_code", string} Tracking number string. Only retrieve trackers with the given tracking code.
        ///   * {"carrier", string} String representing the tracker's carrier. Only retrieve trackers with the given carrier.
        ///   * {"before_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created after this id.
        ///   * {"start_datetime", datetime} Datetime representing the earliest possible tracker. Only retrieve trackers created at or after this datetime. Defaults to 1 month ago.
        ///   * {"end_datetime", datetime} Datetime representing the latest possible tracker. Only retrieve trackers created before this datetime. Defaults to the end of the current day.
        ///   * {"page_size", int} Size of page. Default to 30.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>Instance of EasyPost.ShipmentList</returns>
        public static TrackerList List(Dictionary<string, object> parameters = null, string apiKey = null) {
            Request request = new Request("trackers");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            TrackerList trackerList = request.Execute<TrackerList>(apiKey);
            trackerList.filters = parameters;
            return trackerList;
        }

        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public static Tracker Create(string carrier, string trackingCode, string apiKey = null) {
            Request request = new Request("trackers", RestSharp.Method.POST);
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                { "tracking_code", trackingCode }, { "carrier", carrier }
            };

            request.AddBody(parameters, "tracker");

            return request.Execute<Tracker>(apiKey);
        }

        /// <summary>
        /// Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Tracker instance.</returns>
        public static Tracker Retrieve(string id, string apiKey = null) {
            Request request = new Request("trackers/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Tracker>(apiKey);
        }
    }
}
