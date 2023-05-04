using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.BetaFeatures.Parameters.Trackers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.TrackerService.CreateList(CreateList, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class CreateList : BaseParameters, ITrackerParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The list of <see cref="Models.API.Tracker"/>s to batch-create.
        /// </summary>
        private List<CreateListTracker> Trackers { get; set; }

        /// <summary>
        ///     Construct a new <see cref="CreateList"/> instance.
        /// </summary>
        public CreateList()
        {
            Trackers = new List<CreateListTracker>();
        }

        /// <summary>
        ///     Add a tracker to this parameter set.
        /// </summary>
        /// <param name="trackingCode">Tracking code for the carrier.</param>
        /// <param name="carrier">Name of the carrier, optional.</param>
        public void AddTracker(string trackingCode, string? carrier = null)
        {
            Trackers.Add(new CreateListTracker(trackingCode, carrier));
        }

        /// <summary>
        ///     Override the default <see cref="BaseParameters.ToDictionary"/> method to handle the unique serialization requirements for this parameter set.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        internal override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            Trackers.Each((index, tracker) =>
            {
                Dictionary<string, object> trackerData = new Dictionary<string, object>
                {
                    { "tracking_code", tracker.TrackingCode },
                };
                if (tracker.Carrier != null)
                {
                    trackerData.Add("carrier", tracker.Carrier);
                }

                data.Add(index.ToString(CultureInfo.InvariantCulture), trackerData);
            });

            return new Dictionary<string, object>
            {
                { "trackers", data },
            };
        }

        #endregion
    }

    /// <summary>
    ///     Internal class used to construct a <see cref="CreateList"/> parameter set.
    /// </summary>
    internal sealed class CreateListTracker
    {
        /// <summary>
        ///     The carrier for the <see cref="Models.API.Tracker"/>.
        /// </summary>
        public string? Carrier { get; set; }
        
        /// <summary>
        ///     The tracking code for the <see cref="Models.API.Tracker"/>.
        /// </summary>
        public string TrackingCode { get; set; }

        /// <summary>
        ///     Instantiate a new instance of the <see cref="CreateListTracker"/> class.
        /// </summary>
        /// <param name="trackingCode">The tracking code.</param>
        /// <param name="carrier">The carrier.</param>
        public CreateListTracker(string trackingCode, string? carrier)
        {
            Carrier = carrier;
            TrackingCode = trackingCode;
        }
    }
}
