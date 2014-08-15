using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Tracker : IResource {
        private static Client client = new Client();

        public DateTime created_at { get; set; }
        public string id { get; set; }
        public string mode { get; set; }
        public string shipment_id { get; set; }
        public string status { get; set; }
        public string tracking_code { get; set; }
        public List<TrackingDetail> tracking_details { get; set; }
        public DateTime updated_at { get; set; }

        public static Tracker Create(string carrier, string trackingCode) {
            Request request = new Request("trackers", RestSharp.Method.POST);
            Dictionary<string, object> parameters = new Dictionary<string, object>() { 
                { "tracking_code", trackingCode }, { "carrier", carrier }
            };

            request.addBody(parameters, "tracker");

            return client.Execute<Tracker>(request);
        }
    }
}