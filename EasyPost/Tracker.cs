using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Tracker : IResource {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime tracking_updated_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime est_delivery_date { get; set; }
        public string mode { get; set; }
        public string shipment_id { get; set; }
        public string status { get; set; }
        public string carrier { get; set; }
        public string tracking_code { get; set; }
        public string signed_by { get; set; }
        public double weight { get; set; }
        public List<TrackingDetail> tracking_details { get; set; }

        public static Tracker Create(string carrier, string trackingCode) {
            Request request = new Request("trackers", RestSharp.Method.POST);
            Dictionary<string, object> parameters = new Dictionary<string, object>() { 
                { "tracking_code", trackingCode }, { "carrier", carrier }
            };

            request.addBody(parameters, "tracker");

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