namespace EasyPost
{
    /// <summary>
    ///     Provides configuration options for the REST client.
    /// </summary>
    public class ClientConfiguration
    {
        private const string DefaultBaseUrl = "https://api.easypost.com/v2";

        /// <summary>
        ///     The API base URI to use on a per-request basis.
        /// </summary>
        public string ApiBase { get; set; }
        /// <summary>
        ///     The API key to use on per-request basis.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="apiBase">The base API url to use for the client connection.</param>
        public ClientConfiguration(string apiKey, string? apiBase = null)
        {
            ApiKey = apiKey;
            ApiBase = apiBase ?? DefaultBaseUrl;
        }
    }
}
