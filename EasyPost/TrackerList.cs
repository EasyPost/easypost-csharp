// TrackerList.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace EasyPost
{
    public class TrackerList
    {
        public Dictionary<string, object> filters { get; set; }

        public bool has_more { get; set; }

        public List<Tracker> trackers { get; set; }


        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.List().
        /// </summary>
        /// <returns>A new EasyPost.ShipmentList instance.</returns>
        public TrackerList Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = trackers.Last().id;

            return Tracker.List(filters);
        }
    }
}
