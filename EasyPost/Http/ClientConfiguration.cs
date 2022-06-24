using System;
using System.Diagnostics;
using System.Reflection;
using EasyPost.Clients;

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
        internal readonly ApiVersion ApiVersion;
        private readonly string _dotNetVersion;
        private readonly string _libraryVersion;

        internal string UserAgent => $"EasyPost/{ApiVersion} CSharpClient/{_libraryVersion} .NET/{_dotNetVersion}";

        /// <summary>
        ///     The API version string.
        /// </summary>
        private string ApiVersionString => ApiVersionDetails.FromEnum(ApiVersion);

        /// <summary>
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="apiVersion">Which version of the API to use.</param>
        internal ClientConfiguration(string apiKey, ApiVersion apiVersion)
        {
            ApiKey = apiKey;
            ApiVersion = apiVersion;
            ApiBase = $"https://api.easypost.com/{ApiVersionString}";

            try
            {
                Assembly assembly = typeof(Client).Assembly;
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
                _libraryVersion = info.FileVersion ?? "Unknown";
            }
            catch (Exception)
            {
                _libraryVersion = "Unknown";
            }

            _dotNetVersion = Environment.Version.ToString();
        }
    }
}
