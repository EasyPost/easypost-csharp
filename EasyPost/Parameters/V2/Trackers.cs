using System.Collections.Generic;
using System.Globalization;
using EasyPost.Utilities.Annotations;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.V2
{
    public static class Trackers
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.TrackerService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.TrackerService.CreateList"/> API calls.
        /// </summary>
        public sealed class CreateList : RequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "trackers")]
            public List<EasyPost.Models.API.Tracker>? Trackers { get; set; }

            #endregion

            public override Dictionary<string, object> ToDictionary()
            {
                // Override default behavior to mutate the list of trackers into a dictionary.
                var trackersDictionary = new Dictionary<string, object>
                {
                };
                Trackers?.Each((index, tracker) => { trackersDictionary.Add(index.ToString(CultureInfo.InvariantCulture), tracker); });
                return new Dictionary<string, object>
            {
                {
                    "trackers", trackersDictionary
                },
            };
            }
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.TrackerService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "carrier")]
            public string? Carrier { get; set; }

            [RequestParameter(Necessity.Optional, "tracking_code")]
            public string? TrackingCode { get; set; }

            #endregion
        }
    }
}
