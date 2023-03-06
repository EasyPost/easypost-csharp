using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Reports
{
    public class Create : BaseParameters, IReportParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "report", "additional_columns")]
        public List<string>? AdditionalColumns { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "report", "columns")]
        public List<string>? Columns { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "report", "end_date")]
        public string? EndDate { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "report", "include_children")]
        public bool IncludeChildren { get; set; } = false;

        [TopLevelRequestParameter(Necessity.Optional, "report", "send_email")]
        public bool SendEmail { get; set; } = false;

        [TopLevelRequestParameter(Necessity.Required, "report", "start_date")]
        public string? StartDate { get; set; }

        #endregion
    }
}
