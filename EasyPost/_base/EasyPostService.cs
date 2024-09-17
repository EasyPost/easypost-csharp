using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    /// <summary>
    ///     Base class for all EasyPost services (collection of related methods).
    /// </summary>
    public abstract class EasyPostService : IEasyPostService, IDisposable
    {
        /// <summary>
        ///     The <see cref="EasyPostClient"/> that this service will use to make API requests.
        /// </summary>
        protected internal readonly EasyPostClient Client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EasyPostService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to use when this service makes API requests.</param>
        protected EasyPostService(EasyPostClient client)
        {
            Client = client;
        }

        /// <summary>
        ///     Make an HTTP request to the EasyPost API and deserialize the response JSON into an object.
        /// </summary>
        /// <param name="method">HTTP <see cref="Http.Method"/> to use for the request.</param>
        /// <param name="endpoint">EasyPost API endpoint to use for the request.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <param name="parameters">Optional parameters to use for the request.</param>
        /// <param name="rootElement">Optional JSON key to start at when deserializing the resultant JSON into an object.</param>
        /// <param name="overrideApiVersion">Override the default <see cref="ApiVersion"/> (<see cref="ApiVersion.Current"/>) to use for the request.</param>
        /// <typeparam name="T">Type of object to return from request.</typeparam>
        /// <returns>A T-type object.</returns>
        protected async Task<T> RequestAsync<T>(Http.Method method, string endpoint, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null)
            where T : class
            => await Client.RequestAsync<T>(method, endpoint, overrideApiVersion ?? ApiVersion.Current, cancellationToken, parameters, rootElement).ConfigureAwait(false);

        /// <summary>
        ///     Make a non-response HTTP request to the EasyPost API.
        /// </summary>
        /// <param name="method">HTTP <see cref="Http.Method"/> to use for the request.</param>
        /// <param name="endpoint">EasyPost API endpoint to use for the request.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <param name="parameters">Optional parameters to use for the request.</param>
        /// <param name="overrideApiVersion">Override the default <see cref="ApiVersion"/> (<see cref="ApiVersion.Current"/>) to use for the request.</param>
        /// <returns>None.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        protected async Task RequestAsync(Http.Method method, string endpoint, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, ApiVersion? overrideApiVersion = null) => await Client.RequestAsync(method, endpoint, overrideApiVersion ?? ApiVersion.Current, cancellationToken, parameters).ConfigureAwait(false);

        /// <inheritdoc cref="EasyPostClient._isDisposed"/>
        private bool _isDisposed;

        /// <inheritdoc cref="EasyPostClient.Dispose()"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="EasyPostClient.Dispose(bool)"/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _isDisposed) return;

            // Set the disposed flag to true before disposing of the object to avoid infinite loops
            _isDisposed = true;

            // Dispose managed state (managed objects)

            // Don't dispose of the associated client here, as it may be shared among multiple services
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="EasyPostService"/> class.
        /// </summary>
        ~EasyPostService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(disposing: false);
        }
    }

    /// <summary>
    ///     Interface for any service (collection of related methods) in this SDK.
    /// </summary>
    public interface IEasyPostService
    {
    }
}
