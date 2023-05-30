using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Report
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#report-object">EasyPost report</a>.
    /// </summary>
    public class Report : EasyPostObject, Parameters.IReportParameter
    {
        #region JSON Properties

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("include_children")]
        public bool? IncludeChildren { get; set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? UrlExpiresAt { get; set; }

        #endregion

    }
#pragma warning disable CA1724 // Naming conflicts with Parameters.Report

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Report"/>s.
    /// </summary>
    public class ReportCollection : PaginatedCollection<Report>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Report"/>s in the collection.
        /// </summary>
        [JsonProperty("reports")]
        public List<Report>? Reports { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Report> entries, int? pageSize = null)
        {
            Parameters.Report.All parameters = Filters != null ? (Parameters.Report.All)Filters : new Parameters.Report.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
