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
        ///     Create an EasyPost.ClientConfiguration instance.
        /// </summary>
        /// <param name="apiKey">The API key to use for the client connection.</param>
        /// <param name="apiBase">The base API url to use for the client connection.</param>
        internal ClientConfiguration(string apiKey, string apiBase)
        {
            ApiKey = apiKey;
            ApiBase = apiBase;
        }
    }
}
