using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using RestSharp;

namespace EasyPost.Services
{
    public class Trackers : Service
    {
        public Trackers(ApiClient client) : base(client)
        {
        }

        /// <summary>
        ///     Create a tracker.
        /// </summary>
        /// <param name="carrier">Carrier for the tracker.</param>
        /// <param name="trackingCode">Tracking code for the tracker.</param>
        /// <returns>An EasyPost.Tracker instance.</returns>
        public async Task<Tracker> Create(string carrier, string trackingCode)
        {
            return await Create<Tracker>("trackers", new Dictionary<string, object>
            {
                {
                    "tracker", new Dictionary<string, object>
                    {
                        {
                            "tracking_code", trackingCode
                        },
                        {
                            "carrier", carrier
                        }
                    }
                }
            });
        }


        /// <summary>
        ///     Get a paginated list of trackers.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"tracking_code", string} Tracking number string. Only retrieve trackers with the given tracking code.
        ///     * {"carrier", string} String representing the tracker's carrier. Only retrieve trackers with the given carrier.
        ///     * {"before_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created before
        ///     this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a Tracker. Starts with "trk_". Only retrieve trackers created after this
        ///     id.
        ///     * {"start_datetime", datetime} Datetime representing the earliest possible tracker. Only retrieve trackers created
        ///     at or after this datetime. Defaults to 1 month ago.
        ///     * {"end_datetime", datetime} Datetime representing the latest possible tracker. Only retrieve trackers created
        ///     before this datetime. Defaults to the end of the current day.
        ///     * {"page_size", int} Size of page. Default to 30.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A EasyPost.TrackerCollection instance.</returns>
        public async Task<TrackerCollection> All(Dictionary<string, object>? parameters = null)
        {
            TrackerCollection trackerCollection = await List<TrackerCollection>("trackers", parameters);
            trackerCollection.filters = parameters;
            trackerCollection.Client = Client;
            return trackerCollection;
        }

        /// <summary>
        ///     Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <returns>EasyPost.Tracker instance.</returns>
        public async Task<Tracker> Retrieve(string id)
        {
            return await Get<Tracker>($"trackers/{id}");
        }

        /// <summary>
        ///     Create a list of trackers
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers</param>
        /// <returns>True</returns>
        public async Task<bool> CreateList(Dictionary<string, object> parameters)
        {
            return await CreateBlind("trackers", new Dictionary<string, object>
            {
                {
                    "trackers", parameters
                }
            });
            // This endpoint does not return a response so we return the request was successful
        }
    }
}
