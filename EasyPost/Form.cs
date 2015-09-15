using System;

namespace EasyPost {
    public class Form : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }      
        public string form_url { get; set; }
        public string form_type { get; set; }
        public string mode { get; set; }
    }
}