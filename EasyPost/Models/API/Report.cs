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

        /// <summary>
        ///     The end date of the report period.
        /// </summary>
        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     Whether to include items created by child users in the report.
        /// </summary>
        [JsonProperty("include_children")]
        public bool? IncludeChildren { get; set; }

        /// <summary>
        ///     The start date of the report period.
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        ///     The status of the report.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"new"</description>
        ///         </item>
        ///         <item>
        ///             <description>"available"</description>
        ///         </item>
        ///         <item>
        ///             <description>"failed"</description>
        ///         </item>
        ///         <item>
        ///             <description>"empty"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     A URL to download the report.
        ///     Expires 30 seconds after retrieving this object.
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }

        /// <summary>
        ///     The date and time at which the <see cref="Url"/> will expire.
        /// </summary>
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
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Report> entries, int? pageSize = null)
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
