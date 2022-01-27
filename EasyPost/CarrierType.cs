// CarrierType.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

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
            Request request = new Request("carrier_types");
            return request.Execute<List<CarrierType>>();
        }
    }
}
