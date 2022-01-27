// CarrierDetail.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;

namespace EasyPost
{
    public class CarrierDetail
    {
        public string alternate_identifier { get; set; }

        public string container_type { set; get; }

        public string destination_location { get; set; }

        public string est_delivery_date_local { set; get; }

        public string est_delivery_time_local { set; get; }

        public DateTime? guaranteed_delivery_date { set; get; }

        public DateTime? initial_delivery_attempt { get; set; }

        public string origin_location { get; set; }

        public string service { set; get; }
    }
}
