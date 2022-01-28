// ReportList.cs
// See LICENSE for licensing info.

using System.Collections.Generic;
using System.Linq;

namespace EasyPost
{
    public class ReportList
    {
        public Dictionary<string, object> filters { get; set; }
        public bool has_more { get; set; }
        public List<Report> reports { get; set; }
        public string type { get; set; }

        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.List().
        /// </summary>
        /// <returns>A new EasyPost.ScanFormList instance.</returns>
        public ReportList Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = reports.Last().id;

            return Report.List(type, filters);
        }
    }
}
