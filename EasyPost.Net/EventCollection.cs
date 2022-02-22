using System.Collections.Generic;

namespace EasyPost
{
    public class EventCollection : Resource
    {
        public List<Event> events { get; set; }
        public bool has_more { get; set; }
    }
}
