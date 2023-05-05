using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#trackers">tracker-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TrackerService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackerService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal TrackerService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="Tracker"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-tracker">Related API documentation</a>.
        /// </summary>
        /// <param name="carrier">Carrier for the <see cref="Tracker"/>.</param>
        /// <param name="trackingCode">Tracking code for the <see cref="Tracker"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Tracker"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Tracker> Create(string carrier, string trackingCode, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "carrier", carrier },
                { "tracking_code", trackingCode },
            };
            parameters = parameters.Wrap("tracker");
            return await RequestAsync<Tracker>(Method.Post, "trackers", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Tracker"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-tracker">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Tracker"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Tracker"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Tracker> Create(BetaFeatures.Parameters.Trackers.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Tracker>(Method.Post, "trackers", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Create a list of trackers.
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        [CrudOperations.Create]
        public async Task CreateList(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("trackers");
            // This endpoint does not return a response, so we simply send the request and only throw an exception if the API returns an error.
            await RequestAsync(Method.Post, "trackers/create_list", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a list of trackers.
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        [CrudOperations.Create]
        [Obsolete("This method is deprecated. Please use TrackerService.Create() instead. This method will be removed in a future version.", false)]
        public async Task CreateList(BetaFeatures.Parameters.Trackers.CreateList parameters, CancellationToken cancellationToken = default)
        {
            await RequestAsync(Method.Post, "trackers/create_list", cancellationToken, parameters.ToDictionary());
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
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A EasyPost.TrackerCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<TrackerCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            // TODO: When we adopt parameter objects as the only way to pass parameters, we don't need to do this object -> dictionary -> object conversion to store the filters.
            TrackerCollection collection = await RequestAsync<TrackerCollection>(Method.Get, "trackers", cancellationToken, parameters);
            collection.Filters = BetaFeatures.Parameters.Trackers.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Tracker"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Trackers.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="TrackerCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<TrackerCollection> All(BetaFeatures.Parameters.Trackers.All parameters, CancellationToken cancellationToken = default)
        {
            TrackerCollection collection = await RequestAsync<TrackerCollection>(Method.Get, "trackers", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="TrackerCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="TrackerCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="TrackerCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<TrackerCollection> GetNextPage(TrackerCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<TrackerCollection, BetaFeatures.Parameters.Trackers.All>(async parameters => await All(parameters, cancellationToken), collection.Trackers, pageSize);

        /// <summary>
        ///     Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Tracker instance.</returns>
        [CrudOperations.Read]
        public async Task<Tracker> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Tracker>(Method.Get, $"trackers/{id}", cancellationToken);

        #endregion
    }
}
