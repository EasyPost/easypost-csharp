using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Report : EasyPostObject, IReportParameter
    {
        #region JSON Properties

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; internal set; }
        [JsonProperty("include_children")]
        public bool? IncludeChildren { get; internal set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("url")]
        public string? Url { get; internal set; }
        [JsonProperty("url_expires_at")]
        public DateTime? UrlExpiresAt { get; internal set; }

        #endregion

        internal Report()
        {
        }
    }

    public class ReportCollection : PaginatedCollection<Report>
    {
        #region JSON Properties

        [JsonProperty("reports")]
        public List<Report>? Reports { get; internal set; }

        #endregion

        internal ReportCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Report> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Reports.All parameters = Filters != null ? (BetaFeatures.Parameters.Reports.All)Filters : new BetaFeatures.Parameters.Reports.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
