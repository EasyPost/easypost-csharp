using System.Collections.Generic;

namespace EasyPost
{
    public class AddressCollection : Resource
    {
        public List<Batch> addresses { get; set; }
        public bool has_more { get; set; }
    }
}
