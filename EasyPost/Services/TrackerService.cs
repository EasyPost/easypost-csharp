using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
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
        public async Task<Tracker> Create(Parameters.Tracker.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Tracker>(Method.Post, "trackers", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Create a list of <see cref="Tracker"/>s.
        /// </summary>
        /// <param name="parameters">A dictionary of tracking codes and carriers.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        [CrudOperations.Create]
        [Obsolete("This method is deprecated. Please use TrackerService.Create() instead. This method will be removed in a future version.", false)]
        public async Task CreateList(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("trackers");
            // This endpoint does not return a response, so we simply send the request and only throw an exception if the API returns an error.
            await RequestAsync(Method.Post, "trackers/create_list", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a list of <see cref="Tracker"/>s.
        /// </summary>
        /// <param name="parameters">Parameters to use to create the <see cref="Tracker"/>s.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        [CrudOperations.Create]
        [Obsolete("This method is deprecated. Please use TrackerService.Create() instead. This method will be removed in a future version.", false)]
        public async Task CreateList(Parameters.Tracker.CreateList parameters, CancellationToken cancellationToken = default)
        {
            await RequestAsync(Method.Post, "trackers/create_list", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="Tracker"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-trackers">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">A dictionary of parameters to filter the list of <see cref="Tracker"/>s with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="TrackerCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<TrackerCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            // TODO: When we adopt parameter objects as the only way to pass parameters, we don't need to do this object -> dictionary -> object conversion to store the filters.
            TrackerCollection collection = await RequestAsync<TrackerCollection>(Method.Get, "trackers", cancellationToken, parameters);
            collection.Filters = Parameters.Tracker.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Tracker"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-trackers">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.Tracker.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="TrackerCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<TrackerCollection> All(Parameters.Tracker.All parameters, CancellationToken cancellationToken = default)
        {
            TrackerCollection collection = await RequestAsync<TrackerCollection>(Method.Get, "trackers", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="TrackerCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-trackers">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="TrackerCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="TrackerCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<TrackerCollection> GetNextPage(TrackerCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<TrackerCollection, Parameters.Tracker.All>(async parameters => await All(parameters, cancellationToken), collection.Trackers, pageSize);

        /// <summary>
        ///     Retrieve a <see cref="Tracker"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-tracker">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Tracker"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The requested <see cref="Tracker"/>.</returns>
        [CrudOperations.Read]
        public async Task<Tracker> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Tracker>(Method.Get, $"trackers/{id}", cancellationToken);

        #endregion
    }
}
