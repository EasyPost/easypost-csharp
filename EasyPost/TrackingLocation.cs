using System;

namespace EasyPost {
    public class TrackingLocation {
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
    }
}
