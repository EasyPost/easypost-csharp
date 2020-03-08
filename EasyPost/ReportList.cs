using System.Collections.Generic;
using System.Linq;

namespace EasyPost {
    public class ReportList {
#pragma warning disable IDE1006 // Naming Styles
        public List<Report> reports { get; set; }
        public bool has_more { get; set; }
        public Dictionary<string, object> filters { get; set;  }
        public string type { get; set; }
#pragma warning restore IDE1006 // Naming Styles


        /// <summary>
        /// Get the next page of reports based on the original parameters passed to ReportList.List().
        /// </summary>
        /// <returns>A new EasyPost.ScanFormList instance.</returns>
        public ReportList Next() {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = reports.Last().id;

            return Report.List(type, filters);
        }
    }
}
