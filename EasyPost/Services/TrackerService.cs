using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class TrackerService : EasyPostService
    {
        internal TrackerService(Client client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a tracker.
        /// </summary>
        /// <param name="carrier">Carrier for the tracker.</param>
        /// <param name="trackingCode">Tracking code for the tracker.</param>
        /// <returns>An EasyPost.Tracker instance.</returns>
        [CrudOperations.Create]
        public async Task<Tracker> Create(string carrier, string trackingCode)
        {
            Dictionary<string, object?> parameters = new Dictionary<string, object?>
            {
                {
                    "carrier", carrier
                },
                {
                    "tracking_code", trackingCode
                }
            };
            return await Create<Tracker>("trackers", parameters);
        }

        /// <summary>
        ///     Create a list of trackers
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers</param>
        /// <returns>True</returns>
        [CrudOperations.Create]
        public async Task CreateList(Dictionary<string, object?> parameters)
        {
            parameters = parameters.Wrap("trackers");
            await CreateNoResponse("trackers/create_list", parameters);
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
        [CrudOperations.Read]
        public async Task<TrackerCollection> All(Dictionary<string, object?>? parameters = null)
        {
            TrackerCollection trackerCollection = await List<TrackerCollection>("trackers", parameters);
            trackerCollection.Filters = parameters;
            trackerCollection.Client = Client; // specifically needs a v2 client
            return trackerCollection;
        }

        // This endpoint does not return a response so we return the request was successful
        /// <summary>
        ///     Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <returns>EasyPost.Tracker instance.</returns>
        [CrudOperations.Read]
        public async Task<Tracker> Retrieve(string id)
        {
            return await Get<Tracker>($"trackers/{id}");
        }

        #endregion
    }
}
