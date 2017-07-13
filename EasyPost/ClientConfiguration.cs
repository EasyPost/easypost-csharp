namespace EasyPost {
    /// <summary>
    /// Provides configuration options for the REST client
    /// </summary>
    public class configuration {
        internal const string DefaultBaseUrl = "https://api.easypost.com";

        /// <summary>
        /// The API key to use on per-request basis
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The API base URI to use on a per-request basis
        /// </summary>
        public string ApiBase { get; set; }

        /// <summary>
        /// Create a ClientConfiguration instance
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection</param>
        public configuration(string apiKey) : this(apiKey, DefaultBaseUrl) { }

        /// <summary>
        /// Create an ClientConfiguration instance
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection</param>
        /// <param name="apiBase">The base API url to use for the client connection</param>
        public configuration(string apiKey, string apiBase) {
            ApiKey = apiKey;
            ApiBase = apiBase;
        }
    }
}