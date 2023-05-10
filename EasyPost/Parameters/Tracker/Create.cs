using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Tracker
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-tracker">Parameters</a> for <see cref="EasyPost.Services.TrackerService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ITrackerParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The carrier for the <see cref="Models.API.Tracker"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "tracker", "carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The tracking code for the <see cref="Models.API.Tracker"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "tracker", "tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion
    }
}
