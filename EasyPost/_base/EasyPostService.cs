using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostService : IEasyPostService, IDisposable
    {
        protected internal readonly EasyPostClient Client;

#pragma warning disable IDE0021
        internal EasyPostService(EasyPostClient client)
        {
            Client = client;
        }
#pragma warning restore IDE0021

        /// <summary>
        ///     Make an HTTP request to the EasyPost API and deserialize the response JSON into an object.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="endpoint">EasyPost API endpoint (no base url or API version).</param>
        /// <param name="parameters">Optional parameters to include in the request.</param>
        /// <param name="rootElement">Key to narrow to when deserializing the resultant JSON into an object.</param>
        /// <param name="overrideApiVersion">Override API version hit for HTTP request. Defaults to general availability.</param>
        /// <typeparam name="T">Type of object to return from request.</typeparam>
        /// <returns>A T-type object.</returns>
        protected async Task<T> RequestAsync<T>(Http.Method method, string endpoint, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null)
            where T : class
            => await Client.RequestAsync<T>(method, endpoint, overrideApiVersion ?? ApiVersion.Current, cancellationToken, parameters, rootElement).ConfigureAwait(false);

        /// <summary>
        ///     Make an HTTP request to the EasyPost API.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="url">EasyPost API endpoint (no base url or API version).</param>
        /// <param name="parameters">Optional parameters to include in the request.</param>
        /// <param name="overrideApiVersion">Override API version hit for HTTP request. Defaults to general availability.</param>
        /// <returns>None.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        protected async Task RequestAsync(Http.Method method, string url, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, ApiVersion? overrideApiVersion = null) => await Client.RequestAsync(method, url, overrideApiVersion ?? ApiVersion.Current, cancellationToken, parameters).ConfigureAwait(false);

        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                // Dispose managed state (managed objects).

                // Dispose the client
                Client?.Dispose();
            }

            // Free native resources (unmanaged objects) and override a finalizer below.
            _isDisposed = true;
        }

        ~EasyPostService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(disposing: false);
        }
    }

    public interface IEasyPostService
    {
    }
}
