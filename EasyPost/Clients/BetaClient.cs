using System.Net.Http;
using EasyPost.Interfaces;
using EasyPost.Services.Beta;

namespace EasyPost.Clients
{
    public class BetaClient : BaseClient
    {
        private const string ApiVersion = "beta";

        public PartnerService Partners
        {
            get { return new PartnerService(this); }
        }

            /// <summary>
        ///     Constructor for the Beta EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not advised for general use.</param>
        public BetaClient(string apiKey, HttpClient? customHttpClient = null) : base(apiKey, ApiVersion, customHttpClient)
        {
        }
    }
}
