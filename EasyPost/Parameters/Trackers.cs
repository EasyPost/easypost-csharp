using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Utilities;

namespace EasyPost.Parameters
{
    public static class Trackers
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "tracker", "carrier")]
            public string? Carrier { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "tracker", "tracking_code")]
            public string? TrackingCode { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "tracker", "amount")]
            public string? Amount { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "options", "carrier_account")]
            public string? CarrierAccount { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "options", "is_return")]
            public bool IsReturn { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "options", "full_test_tracker")]
            public bool FullTestTracker { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class CreateList : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "trackers")]
            public List<Tracker>? Trackers { internal get; set; }

            #endregion

            public CreateList(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                // TODO: This custom overload does not check for API compatibility.

                // TODO: Please, can we fix this hack in the API?
                Dictionary<string, object> trackersDictionary = new Dictionary<string, object>
                {
                };
                Trackers?.Each((index, tracker) => { trackersDictionary.Add(index.ToString(), tracker); });
                return new Dictionary<string, object?>
                {
                    {
                        "trackers", trackersDictionary
                    }
                };
            }
        }
    }
}
