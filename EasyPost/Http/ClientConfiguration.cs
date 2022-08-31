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
        public readonly string ApiBase;

        /// <summary>
        ///     The API key.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        public readonly string ApiKey;

        /// <summary>
        ///     The API version.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
        public readonly ApiVersion ApiVersion;

        /// <summary>
        ///     A custom HttpClient to use for requests.
        ///     This cannot be changed after the client has been initialized.
        /// </summary>
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
        ///     The request timeout in milliseconds set by the user.
        /// </summary>
        private int? _requestTimeoutMilliseconds;

        /// <summary>
        ///     The connect timeout in milliseconds.
        /// </summary>
        public int ConnectTimeoutMilliseconds
        {
            get => _connectTimeoutMilliseconds ?? Defaults.DefaultConnectTimeoutMilliseconds;
            set => _connectTimeoutMilliseconds = value;
        }

        /// <summary>
        ///     The request timeout in milliseconds.
        /// </summary>
        public int RequestTimeoutMilliseconds
        {
            get => _requestTimeoutMilliseconds ?? Defaults.DefaultRequestTimeoutMilliseconds;
            set => _requestTimeoutMilliseconds = value;
        }

        //TODO: User-Agent will not show beta vs GA, will always show latest GA version, even when using beta endpoint
        internal string UserAgent => $"EasyPost/{ApiVersion.Value} CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

        /// <summary>
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="apiVersion">API version to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`</param>
        /// <param name="customHttpClient">The custom HTTP client to use for the client connection.</param>
        internal ClientConfiguration(string apiKey, ApiVersion apiVersion, string? baseUrl, HttpClient? customHttpClient = null)
        {
            ApiKey = apiKey;
            ApiBase = baseUrl ?? Defaults.DefaultBaseUrl;
            ApiVersion = apiVersion;
            HttpClient = customHttpClient;

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

            /// <summary>
            ///     The default request timeout in milliseconds.
            /// </summary>
            internal const int DefaultRequestTimeoutMilliseconds = 60000;
        }
    }
}
