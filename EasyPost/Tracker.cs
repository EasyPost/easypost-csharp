using System;
using System.Collections.Generic;

namespace EasyPost
{
    public class Tracker : IResource
    {
        private static Client client = new Client();

        public DateTime created_at { get; set; }

        public string id { get; set; }

        public string mode { get; set; }

        public string shipment_id { get; set; }

        public string status { get; set; }

        public string tracking_code { get; set; }

        public List<TrackingDetail> tracking_details { get; set; }

        public DateTime updated_at { get; set; }

        public static Tracker Retrieve(string carrier, string trackingCode)
        {
            Request request = new Request("trackers", RestSharp.Method.POST);
            var requestParams = new Dictionary<string, object>();
           
            requestParams.Add("tracking_code", trackingCode);
            requestParams.Add("carrier", carrier);
            request.addBody(requestParams, "tracker");

            return client.Execute<Tracker>(request);
        }
    }
}