using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Tracker : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public DateTime tracking_updated_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public Nullable<DateTime> est_delivery_date { get; set; }
        public string mode { get; set; }
        public string shipment_id { get; set; }
        public string status { get; set; }
        public string carrier { get; set; }
        public string tracking_code { get; set; }
        public string signed_by { get; set; }
        public Nullable<double> weight { get; set; }
        public List<TrackingDetail> tracking_details { get; set; }

        /// <summary>
        /// Get a paginated list of trackers.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created after this id.
        ///   * {"page_size", int} Size of page. Default to 20.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <returns>Instance of EasyPost.ShipmentList</returns>
        public static TrackerList List(Dictionary<string, object> parameters = null) {
            Request request = new Request("trackers");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            TrackerList trackerList = request.Execute<TrackerList>();
            trackerList.filters = parameters;
            return trackerList;
        }

        public static Tracker Create(string carrier, string trackingCode) {
            Request request = new Request("trackers", RestSharp.Method.POST);
            Dictionary<string, object> parameters = new Dictionary<string, object>() { 
                { "tracking_code", trackingCode }, { "carrier", carrier }
            };

            request.AddBody(parameters, "tracker");

            return request.Execute<Tracker>();
        }

        /// <summary>
        /// Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <returns>EasyPost.Tracker instance.</returns>
        public static Tracker Retrieve(string id) {
            Request request = new Request("trackers/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Tracker>();
        }
    }
}