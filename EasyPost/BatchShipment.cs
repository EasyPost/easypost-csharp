using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class BatchShipment : IResource {
        public string id { get; set; }
        public string batch_status { get; set; }
        public string batch_message { get; set; }
    }
}
