using System;

namespace EasyPost
{
    public class Forms : IResource
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }      
        public string form_url { get; set; }
        public string form_type { get; set; }
        public string mode { get; set; }
    }
}
