﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class ScanFormList : IResource {
        public List<ScanForm> scanForms { get; set; }
        public bool has_more { get; set; }

        public Dictionary<string, object> filters { get; set; }

        /// <summary>
        /// Get the next page of scan forms based on the original parameters passed to ScanForm.List().
        /// </summary>
        /// <returns>A new EasyPost.ScanFormList instance.</returns>
        public ScanFormList Next() {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = scanForms.Last().id;

            return ScanForm.List(filters);
        }
    }
}