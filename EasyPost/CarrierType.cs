using System.Collections.Generic;

namespace EasyPost
{
    public class CarrierType : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public string type { get; set; }
        public string readable { get; set; }
        public string logo { get; set; }
        public Dictionary<string, object> fields { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>
        public static List<CarrierType> All()
        {
            Request request = new Request("v2/carrier_types");
            return request.Execute<List<CarrierType>>();
        }
    }
}