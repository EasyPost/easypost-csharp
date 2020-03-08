using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost {
    public class ScanFormList : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public List<ScanForm> scan_forms { get; set; }
        public bool has_more { get; set; }
        public Dictionary<string, object> filters { get; set; }
#pragma warning restore IDE1006 // Naming Styles


        /// <summary>
        /// Get the next page of scan forms based on the original parameters passed to ScanForm.List().
        /// </summary>
        /// <returns>A new EasyPost.ScanFormList instance.</returns>
        public ScanFormList Next() {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = scan_forms.Last().id;

            return ScanForm.List(filters);
        }
    }
}