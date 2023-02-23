using System.Collections.Generic;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class Reports
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.ReportService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "report", "additional_columns")]
            public List<string>? AdditionalColumns { get; set; }

            [RequestParameter(Necessity.Optional, "report", "columns")]
            public List<string>? Columns { get; set; }

            [RequestParameter(Necessity.Optional, "report", "end_date")]
            public string? EndDate { get; set; }

            [RequestParameter(Necessity.Optional, "report", "include_children")]
            public bool IncludeChildren { get; set; } = false;

            [RequestParameter(Necessity.Optional, "report", "send_email")]
            public bool SendEmail { get; set; } = false;

            [RequestParameter(Necessity.Optional, "report", "start_date")]
            public string? StartDate { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.ReportService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
