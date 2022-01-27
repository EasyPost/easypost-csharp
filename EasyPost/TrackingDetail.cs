// TrackingDetail.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;

namespace EasyPost
{
    public class TrackingDetail : Resource
    {
        public DateTime? datetime { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public TrackingLocation tracking_location { get; set; }
    }
}
