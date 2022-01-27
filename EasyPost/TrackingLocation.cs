// TrackingLocation.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;

namespace EasyPost
{
    public class TrackingLocation
    {
        public string city { get; set; }

        public string country { get; set; }

        public DateTime? created_at { get; set; }

        public string state { get; set; }

        public DateTime? updated_at { get; set; }

        public string zip { get; set; }
    }
}
