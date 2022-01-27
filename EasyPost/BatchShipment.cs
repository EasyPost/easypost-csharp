﻿// BatchShipment.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

namespace EasyPost
{
    public class BatchShipment : Resource
    {
        public string batch_message { get; set; }

        public string batch_status { get; set; }

        public string id { get; set; }

        public string tracking_code { get; set; }
    }
}
