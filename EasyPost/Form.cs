using System;

namespace EasyPost {
    public class Form : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string form_url { get; set; }
        public string form_type { get; set; }
        public string mode { get; set; }
        public bool submitted_electronically { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}