using System.Net.Http;
using EasyPost._base;
using EasyPost.Services.Beta;

namespace EasyPost
{
    public class BetaClient : EasyPostClient
    {
        public EndShipperService EndShipper => GetService<EndShipperService>();

        public PartnerService Partner => GetService<PartnerService>();

        /// <summary>
        ///     Constructor for the EasyPost beta client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. Must include API version.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed.</param>
        internal BetaClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null) : base(apiKey, ApiVersion.Beta, baseUrl, customHttpClient)
        {
        }
    }
}
