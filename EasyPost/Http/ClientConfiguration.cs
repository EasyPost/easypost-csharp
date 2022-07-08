using EasyPost.Clients;
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
        internal readonly string ApiBase;
        /// <summary>
        ///     The API key.
        /// </summary>
        internal readonly string ApiKey;

        /// <summary>
        ///     The API version.
        /// </summary>
        internal readonly ApiVersionDetails ApiVersionDetails;

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

        internal string UserAgent => $"EasyPost/v2 CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

        /// <summary>
        ///     The API version string.
        /// </summary>
        private string ApiVersionString => ApiVersionDetails.Prefix;

        /// <summary>
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="apiVersion">Which version of the API to use.</param>
        internal ClientConfiguration(string apiKey, ApiVersion apiVersion)
        {
            ApiKey = apiKey;
            ApiVersionDetails = ApiVersionDetails.GetApiVersionDetails(apiVersion);
            ApiBase = $"https://api.easypost.com/{ApiVersionString}";

            _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
            _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
            _osName = RuntimeInfo.OperationSystemInfo.Name;
            _osVersion = RuntimeInfo.OperationSystemInfo.Version;
            _osArch = RuntimeInfo.OperationSystemInfo.Architecture;
        }
    }
}
