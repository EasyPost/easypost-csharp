using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Tracker {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<TrackingDetail> tracking_details { get; set; }
        public string tracking_code { get; set; }
        public string status { get; set; }
        public string shipment_id { get; set; }
        public string mode { get; set; }
    }
}
