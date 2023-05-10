using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Reports
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-report">Parameters</a> for <see cref="EasyPost.Services.ReportService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IReportParameter
    {
        #region Request Parameters

        /// <summary>
        ///     What type of <see cref="Models.API.Report"/> to create. Required.
        /// </summary>
        // This parameter is not included in the request body, but is used to determine the endpoint to call.
        public string? Type { get; set; }

        /// <summary>
        ///     A list of additional columns (other than the defaults) to include in the report.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "report", "additional_columns")]
        public List<string>? AdditionalColumns { get; set; }

        /// <summary>
        ///     A list of the only columns to include in the report.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "report", "columns")]
        public List<string>? Columns { get; set; }

        /// <summary>
        ///     The date the report should end on. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "report", "end_date")]
        public string? EndDate { get; set; }

        /// <summary>
        ///     Whether or not to include children in the report. Defaults to <c>false</c>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "report", "include_children")]
        public bool? IncludeChildren { get; set; }

        /// <summary>
        ///     Send the report via email. Defaults to <c>false</c>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "report", "send_email")]
        public bool? SendEmail { get; set; }

        /// <summary>
        ///     The date the report should start on. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "report", "start_date")]
        public string? StartDate { get; set; }

        #endregion
    }
}
