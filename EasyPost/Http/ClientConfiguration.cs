using System.Net.Http;
using EasyPost._base;
using EasyPost.Utilities;

namespace EasyPost.Http
{
    /// <summary>
    ///     Provides configuration options for the REST client. Used internally to store API key and other configuration
    /// </summary>
    public class ClientConfiguration
    {
        /// <summary>
        ///     The API base URI.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        // This cannot be changed, because the internal RestClient is initialized with this value immediately.
        public readonly string ApiBase;

        /// <summary>
        ///     The API key.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        // This can be changed between API calls, but only by internal methods (by the library and test suite, but not by the end user).
        public string ApiKey { get; internal set; }

        /// <summary>
        ///     A custom HttpClient to use for requests.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        // This cannot be changed, because the internal RestClient is initialized with this value immediately.
        public readonly HttpClient? HttpClient;

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

        /// <summary>
        ///     The connect timeout in milliseconds set by the user.
        /// </summary>
        private int? _connectTimeoutMilliseconds;

        /// <summary>
        ///     The connect timeout in milliseconds.
        /// </summary>
        public int ConnectTimeoutMilliseconds
        {
            get => _connectTimeoutMilliseconds ?? Defaults.DefaultConnectTimeoutMilliseconds;
            set => _connectTimeoutMilliseconds = value;
        }

        /*
         * NOTE: User-Agent will always show the general availability API version, even if the API call itself goes to a different API version (i.e. beta).
         * This is because the User-Agent must be set when the client is initialized, and the target API version is not known until a request is made.
         */
        internal string UserAgent => $"EasyPost/{ApiVersion.Current.Value} CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

        /// <summary>
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`</param>
        internal ClientConfiguration(string apiKey, string? baseUrl)
        {
            ApiKey = apiKey;
            ApiBase = baseUrl ?? Defaults.DefaultBaseUrl;

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

        public override bool Equals(object? obj)
        {
            if (obj is ClientConfiguration other)
            {
                return ApiKey == other.ApiKey &&
                       ApiBase == other.ApiBase;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ApiKey.GetHashCode() ^ ApiBase.GetHashCode() ^ (HttpClient?.GetHashCode() ?? 1);
        }
    }
}
