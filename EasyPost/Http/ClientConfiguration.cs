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
        // This cannot be changed after the client has been initialized.
        public string ApiBase { get; set; } = "https://api.easypost.com"; // default to production if not specified by the user

        /// <summary>
        ///     A custom HttpClient to use for requests.
        /// </summary>
        // This cannot be changed after the client has been initialized, and is stored for reference only.
        public HttpClient? CustomHttpClient { get; set; } // default to null if not specified by the user
        
        /// <summary>
        ///     The timeout to use for requests.
        /// </summary>
        // This cannot be changed after the client has been initialized.
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60); // default to 60 seconds if not specified by the user

        /// <summary>
        ///     Gets the HttpClient to use for requests.
        ///     This is the HttpClient with the connect timeout set.
        /// </summary>
        // This is the prepared HttpClient that is actually used to make requests, will be initialized when the client is initialized (will never be null).
        internal HttpClient? PreparedHttpClient;

        /// <summary>
        ///     Gets the API key.
        /// </summary>
        // This cannot be changed after the client has been initialized.
        internal string ApiKey;

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
        ///     Get the User-Agent string to use for a request.
        /// </summary>
        /// <param name="apiVersion">Version of the API being used for this request.</param>
        /// <returns>The prepared User-Agent string.</returns>
        private string GetUserAgent(ApiVersion apiVersion) => $"EasyPost/{apiVersion.Value} CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

        /// <summary>
        ///     Gets the headers to use for a request.
        /// </summary>
        internal Dictionary<string, string> GetHeaders(ApiVersion apiVersion) => new()
        {
            { "Authorization", $"Bearer {ApiKey}" },
            { "User-Agent", GetUserAgent(apiVersion) },
            // Content-Type is set downstream while constructing the request body
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientConfiguration"/> class.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client.</param>
        public ClientConfiguration(string apiKey)
        {
            // Required constructor parameters
            ApiKey = apiKey;
            
            // Optional constructor parameters with defaults, set by the constructor
            
            // Calculate the properties that are used in the User-Agent string once and store them
            _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
            _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
            _osName = RuntimeInfo.OperationSystemInfo.Name;
            _osVersion = RuntimeInfo.OperationSystemInfo.Version;
            _osArch = RuntimeInfo.OperationSystemInfo.Architecture;
        }

        /// <summary>
        ///     Sets up the HTTP client.
        ///     Because we need to wait for construction to finish, we have to do this in a separate method
        /// </summary>
        internal void SetUp()
        {
            // Prepare the HttpClient
            PreparedHttpClient = CustomHttpClient ?? new HttpClient(); // copy the custom HttpClient if it exists, otherwise create a new one
            PreparedHttpClient.Timeout = Timeout;
        }

        public override bool Equals(object? obj) => obj is ClientConfiguration other && ApiKey == other.ApiKey && ApiBase == other.ApiBase;

        // ReSharper disable once NonReadonlyMemberInGetHashCode
#pragma warning disable CA1307
        public override int GetHashCode() => ApiKey.GetHashCode() ^ ApiBase.GetHashCode() ^ Timeout.GetHashCode();
#pragma warning restore CA1307
    }
    
    namespace System.Runtime.CompilerServices
    {
        internal static class IsExternalInit {}
    }
}
