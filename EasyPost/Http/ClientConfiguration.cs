using System;
using System.Collections.Generic;
using System.Net.Http;
using EasyPost._base;
using EasyPost.Utilities.Internal;

namespace EasyPost.Http
{
    /// <summary>
    ///     Provides configuration options for the REST client. Used internally to store API key and other configuration.
    /// </summary>
    public class ClientConfiguration
    {
        /// <summary>
        ///     The API base URI.
        /// </summary>
        // This can be changed between API calls by the end user.
        public string ApiBase { get; set; }

        /// <summary>
        ///     Gets the API key.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        // This can be changed between API calls, but only by internal methods (by the library and test suite, but not by the end user).
        public string ApiKey { get; internal set; }

        /// <summary>
        ///     A custom HttpClient to use for requests.
        /// </summary>
        // This cannot be changed after the client has been initialized, and is stored for reference only.
        internal HttpClient HttpClient { get; }

        /// <summary>
        ///     Gets the HttpClient to use for requests.
        ///     This is the HttpClient with the connect timeout set.
        /// </summary>
        internal HttpClient PreparedHttpClient { get; }

        /// <summary>
        ///    The .NET version of the current application.
        /// </summary>
        private readonly string _dotNetVersion;

        /// <summary>
        ///    The version of this library.
        /// </summary>
        private readonly string _libraryVersion;

        /// <summary>
        ///    The architecture of the current application's operating system.
        /// </summary>
        private readonly string _osArch;

        /// <summary>
        ///     The name of the current application's operating system.
        /// </summary>
        private readonly string _osName;

        /// <summary>
        ///     The version of the current application's operating system.
        /// </summary>
        private readonly string _osVersion;

        /*
         * NOTE: User-Agent will always show the general availability API version, even if the API call itself goes to a different API version (i.e. beta).
         * This is because the User-Agent must be set when the client is initialized, and the target API version is not known until a request is made.
         */
        private string UserAgent => $"EasyPost/{ApiVersion.Current.Value} CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

        /// <summary>
        ///     Gets the headers to use for a request.
        /// </summary>
        internal Dictionary<string, string> Headers => new()
        {
            { "Authorization", $"Bearer {ApiKey}" },
            { "User-Agent", UserAgent },
            // Content-Type is set downstream while constructing the request body
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration"/> class.
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`.</param>
        /// <param name="timeoutMilliseconds">Timeout length, in milliseconds, for API calls.</param>
        /// <param name="customHttpClient">The custom HTTP client to use for the client connection.</param>
        internal ClientConfiguration(string apiKey, string? baseUrl, int? timeoutMilliseconds = null, HttpClient? customHttpClient = null)
        {
            // Required constructor parameters
            ApiKey = apiKey;

            // Optional constructor parameters with defaults
            ApiBase = baseUrl ?? Defaults.DefaultBaseUrl;
            HttpClient = customHttpClient ?? new HttpClient();

            // Prepare the HttpClient
            PreparedHttpClient = HttpClient;
            PreparedHttpClient.Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds ?? Defaults.DefaultConnectTimeoutMilliseconds);

            _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
            _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
            _osName = RuntimeInfo.OperationSystemInfo.Name;
            _osVersion = RuntimeInfo.OperationSystemInfo.Version;
            _osArch = RuntimeInfo.OperationSystemInfo.Architecture;
        }

        private abstract class Defaults
        {
            /// <summary>
            ///     The default API base URI.
            /// </summary>
            internal const string DefaultBaseUrl = "https://api.easypost.com";

            /// <summary>
            ///     The default connection timeout in milliseconds.
            /// </summary>
            internal const int DefaultConnectTimeoutMilliseconds = 30000;
        }

        public override bool Equals(object? obj) => obj is ClientConfiguration other && ApiKey == other.ApiKey && ApiBase == other.ApiBase;

        // ReSharper disable once NonReadonlyMemberInGetHashCode
#pragma warning disable CA1307
        public override int GetHashCode() => ApiKey.GetHashCode() ^ ApiBase.GetHashCode() ^ (HttpClient?.GetHashCode() ?? 1);
#pragma warning restore CA1307
    }
}
