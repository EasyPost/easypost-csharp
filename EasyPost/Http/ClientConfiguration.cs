using System.Net.Http;
using EasyPost._base;
using EasyPost.Utilities;

namespace EasyPost.Http
{
    /// <summary>
    ///     Provides configuration options for the REST client. Used internally to store API key and other configuration
    /// </summary>
    internal class ClientConfiguration
    {
        /// <summary>
        ///     The API base URI.
        /// </summary>
        private const string DefaultBaseUrl = "https://api.easypost.com";

        /// <summary>
        ///     The API key.
        /// </summary>
        internal readonly string ApiKey;

        private readonly string? _apiBase;

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

        internal string ApiBase
        {
            get { return _apiBase ?? DefaultBaseUrl; }
        }

        // User-Agent will not show beta vs GA, will always show latest GA version, even when using beta endpoint
        internal string UserAgent => $"EasyPost/{ApiVersion.General.Value} CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

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
            _apiBase = baseUrl;

            _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
            _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
            _osName = RuntimeInfo.OperationSystemInfo.Name;
            _osVersion = RuntimeInfo.OperationSystemInfo.Version;
            _osArch = RuntimeInfo.OperationSystemInfo.Architecture;
        }
    }
}
