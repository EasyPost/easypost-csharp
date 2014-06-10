using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class TrackingDetail : IResource {
        public DateTime datetime { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
}
