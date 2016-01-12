using System;

namespace EasyPost {
    public class TrackingDetail : IResource {
        public Nullable<DateTime> datetime { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public TrackingLocation tracking_location { get; set; }
    }
}
