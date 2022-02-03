using System.Collections.Generic;

namespace EasyPost
{
    public class BatchCollection
    {
        public List<Batch> batches { get; set; }
        public bool has_more { get; set; }
    }
}
