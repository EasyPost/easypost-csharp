using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class ScanForm {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<string> tracking_codes { get; set; }
        public string address { get; set; }
        public string form_url { get; set; }
        public string form_file_type { get; set; }
    }
}
