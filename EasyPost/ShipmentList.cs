// <copyright file="ShipmentList.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace EasyPost
{
    public class ShipmentList : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public List<Shipment> shipments { get; set; }
        public bool has_more { get; set; }
        public Dictionary<string, object> filters { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Get the next page of shipments based on the original parameters passed to Shipment.List().
        /// </summary>
        /// <returns>A new EasyPost.ShipmentList instance.</returns>
        public ShipmentList Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = shipments.Last().id;

            return Shipment.List(filters);
        }
    }
}
