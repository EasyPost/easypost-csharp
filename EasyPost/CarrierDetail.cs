// <copyright file="CarrierDetail.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System;

namespace EasyPost
{
    public class CarrierDetail
    {
        public string alternate_identifier { get; set; }
        public string container_type { get; set; }
        public string destination_location { get; set; }
        public string est_delivery_date_local { get; set; }
        public string est_delivery_time_local { get; set; }
        public DateTime? guaranteed_delivery_date { get; set; }
        public DateTime? initial_delivery_attempt { get; set; }
        public string origin_location { get; set; }
        public string service { get; set; }
    }
}
