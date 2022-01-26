using System;

namespace EasyPost
{
    public class TrackingLocation
    {
#pragma warning disable IDE1006 // Naming Styles
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}