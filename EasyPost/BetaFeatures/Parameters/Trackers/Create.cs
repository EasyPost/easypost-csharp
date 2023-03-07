using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Trackers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.TrackerService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ITrackerParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "tracker", "carrier")]
        public string? Carrier { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "tracker", "tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion
    }
}
