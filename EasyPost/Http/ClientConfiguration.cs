using System;
using System.Net.Http;
using EasyPost._base;
using EasyPost.Utilities.Internal;

namespace EasyPost.Http
{
    /// <summary>
    ///     Provides configuration options for the REST client. Used internally to store API key and other configuration.
    /// </summary>
    public class ClientConfiguration : IDisposable
    {
        /// <summary>
        ///     Gets the API key.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        // This can be changed between API calls, but only by internal methods (by the library and test suite, but not by the end user).
        internal readonly string ApiKey; // internal so users can't set it via a constructor property, only via the parameter

        /// <summary>
        ///     Gets the base URL for the API.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        // This cannot be changed, because the internal RestClient is initialized with this value immediately.
        public string ApiBase = "https://api.easypost.com"; // public so users can set via a constructor property

        /// <summary>
        ///     Gets or sets the connect timeout in milliseconds.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        public int ConnectTimeoutMilliseconds = 30000; // public so users can set via a constructor property

        /// <summary>
        ///     Gets or sets a custom HTTP client used to make requests.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        public HttpClient? CustomHttpClient; // public so users can set via a constructor property (stored for auditing and cloning purposes)

        /// <summary>
        ///     Gets or sets the prepared HTTP client used to make requests.
        ///     This is the client actually used to make requests.
        /// </summary>
        internal HttpClient? PreparedHttpClient; // the actual HttpClient used to make requests, this will never actually be null

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
        ///     Initializes a new instance of the <see cref="ClientConfiguration"/> class.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        internal ClientConfiguration(string apiKey)
        {
            // set required values
            ApiKey = apiKey;

            // optional values (HttpClient, ConnectTimeoutMilliseconds) are set to defaults during construction if not set by the user

            // set internal user agent values
            _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
            _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
            _osName = RuntimeInfo.OperationSystemInfo.Name;
            _osVersion = RuntimeInfo.OperationSystemInfo.Version;
            _osArch = RuntimeInfo.OperationSystemInfo.Architecture;
        }

        internal void SetUpClient()
        {
            // set up the HttpClient
            PreparedHttpClient = CustomHttpClient ?? new HttpClient();
            PreparedHttpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent); // we set the user agent here once so it's not needlessly calculated for every request
            PreparedHttpClient.Timeout = new TimeSpan(0, 0, 0, 0, milliseconds: ConnectTimeoutMilliseconds); // set the default timeout for the client
        }

        public override bool Equals(object? obj) => obj is ClientConfiguration other && ApiKey == other.ApiKey && ApiBase == other.ApiBase;

        // ReSharper disable once NonReadonlyMemberInGetHashCode
#pragma warning disable CA1307
        public override int GetHashCode() => ApiKey.GetHashCode() ^ ApiBase.GetHashCode();
#pragma warning restore CA1307
        public void Dispose()
        {
            // dispose of the prepared HTTP client
            PreparedHttpClient?.Dispose();

            // dispose of the user-provided HTTP client
            CustomHttpClient?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
