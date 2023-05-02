using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Reports
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ReportService.Create(string, Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IReportParameter
    {
        #region Request Parameters

        /// <summary>
        ///     What type of report to create. Required.
        /// </summary>
        // This parameter is not included in the request body, but is used to determine the endpoint to call.
        public string? Type { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "report", "additional_columns")]
        public List<string>? AdditionalColumns { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "report", "columns")]
        public List<string>? Columns { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "report", "end_date")]
        public string? EndDate { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "report", "include_children")]
        public bool? IncludeChildren { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "report", "send_email")]
        public bool? SendEmail { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "report", "start_date")]
        public string? StartDate { get; set; }

        #endregion
    }
}
