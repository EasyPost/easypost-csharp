using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.Tracker
{
    /// <summary>
    ///     This parameter set is no longer used.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Obsolete("This parameter set is no longer used.")]
    public class CreateList : BaseParameters<Models.API.Tracker>, ITrackerParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The list of <see cref="Models.API.Tracker"/>s to batch-create.
        /// </summary>
        private List<CreateListTracker> Trackers { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateList"/> class.
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
        ///     Override the default <see cref="BaseParameters{TMatchInputType}.ToDictionary"/> method to handle the unique serialization requirements for this parameter set.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public override Dictionary<string, object> ToDictionary()
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
    ///     This class is no longer used.
    /// </summary>
    [Obsolete("This class is no longer used.")]
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
        ///     Initializes a new instance of the <see cref="CreateListTracker"/> class.
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
