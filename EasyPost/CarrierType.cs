using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class CarrierType {
        public string type { get; set; }
        public string readable { get; set; }
        public string logo { get; set; }
        public Dictionary<string, object> fields { get; set; }

        public static List<CarrierType> All() {
            Request request = new Request("carrier_types");
            return request.Execute<List<CarrierType>>();
        }
    }
}