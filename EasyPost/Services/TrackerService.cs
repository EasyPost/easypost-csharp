using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TrackerService : EasyPostService
    {
        internal TrackerService(EasyPostClient client)
            : base(client)
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
            Dictionary<string, object> parameters = new()
            {
                { "carrier", carrier },
                { "tracking_code", trackingCode },
            };
            parameters = parameters.Wrap("tracker");
            return await Request<Tracker>(Method.Post, "trackers", parameters);
        }

        /// <summary>
        ///     Create a <see cref="Tracker"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Trackers.Create"/> parameter set.</param>
        /// <returns><see cref="Tracker"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Tracker> Create(BetaFeatures.Parameters.Trackers.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<Tracker>(Method.Post, "trackers", parameters.ToDictionary());
        }

        /// <summary>
        ///     Create a list of trackers.
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers.</param>
        /// <returns>True if successful, False otherwise.</returns>
        [CrudOperations.Create]
        public async Task CreateList(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("trackers");
            // This endpoint does not return a response, so we simply send the request and only throw an exception if the API returns an error.
            await Request(Method.Post, "trackers/create_list", parameters);
        }

        /// <summary>
        ///     Create a list of trackers.
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers.</param>
        /// <returns>True if successful, False otherwise.</returns>
        [CrudOperations.Create]
        [Obsolete("This method is deprecated. Please use TrackerService.Create() instead. This method will be removed in a future version.", false)]
        public async Task CreateList(BetaFeatures.Parameters.Trackers.CreateList parameters)
        {
            await Request(Method.Post, "trackers/create_list", parameters.ToDictionary());
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
        public async Task<TrackerCollection> All(Dictionary<string, object>? parameters = null)
        {
            TrackerCollection trackerCollection = await Request<TrackerCollection>(Method.Get, "trackers", parameters);
            trackerCollection.TrackingCode = parameters?.GetOrNull<string>("tracking_code");
            trackerCollection.Carrier = parameters?.GetOrNull<string>("carrier");
            return trackerCollection;
        }

        /// <summary>
        ///     List all <see cref="Tracker"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Trackers.All"/> parameter set.</param>
        /// <returns><see cref="TrackerCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<TrackerCollection> All(BetaFeatures.Parameters.Trackers.All parameters)
        {
            TrackerCollection trackerCollection = await Request<TrackerCollection>(Method.Get, "trackers", parameters.ToDictionary());
            trackerCollection.TrackingCode = parameters.TrackingCode;
            trackerCollection.Carrier = parameters.Carrier;
            return trackerCollection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="TrackerCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="TrackerCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="TrackerCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<TrackerCollection> GetNextPage(TrackerCollection collection, int? pageSize = null) => await collection.GetNextPage<TrackerCollection, BetaFeatures.Parameters.Trackers.All>(async parameters => await All(parameters), collection.Trackers, pageSize);

        /// <summary>
        ///     Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <returns>EasyPost.Tracker instance.</returns>
        [CrudOperations.Read]
        public async Task<Tracker> Retrieve(string id) => await Request<Tracker>(Method.Get, $"trackers/{id}");

        #endregion
    }
}
