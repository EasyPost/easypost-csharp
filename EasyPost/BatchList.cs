using System.Collections.Generic;
using System.Linq;

namespace EasyPost
{
    public class BatchList : Resource
    {
        public List<Batch> batches { get; set; }
        public bool has_more { get; set; }

        public Dictionary<string, object> filters { get; set; }

        /// <summary>
        /// Get the next page of batches based on the original parameters passed to Batch.List().
        /// </summary>
        /// <returns>A new EasyPost.BatchList instance.</returns>
        public BatchList Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = batches.Last().id;

            return Batch.List(filters);
        }
    }
}