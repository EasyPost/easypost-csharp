// <copyright file="CarrierType.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace EasyPost
{
    public class CarrierType : Resource
    {
        public Dictionary<string, object> fields { get; set; }
        public string logo { get; set; }
        public string readable { get; set; }
        public string type { get; set; }

        /// <summary>
        ///     Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>
        public static List<CarrierType> All()
        {
            var request = new Request("carrier_types");
            return request.Execute<List<CarrierType>>();
        }
    }
}
