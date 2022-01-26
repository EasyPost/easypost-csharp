using System;

namespace EasyPost
{
    public class TrackingDetail : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public DateTime? datetime { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public TrackingLocation tracking_location { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
