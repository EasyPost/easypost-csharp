using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/luma">luma-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LumaService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LumaService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal LumaService(EasyPostClient client)
            : base(client)
        {
        }

        #region Luma Operations

        /// <summary>
        ///     Get service recommendations from Luma that meet the criteria of your ruleset.
        /// </summary>
        /// <param name="parameters">Dictionary of Luma parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="LumaInfo"/> object.</returns>
        public async Task<LumaInfo> GetPromise(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("shipment");
            return await RequestAsync<LumaInfo>(Method.Post, "luma/promise", cancellationToken, parameters, "luma_info");
        }

        /// <summary>
        ///     Get service recommendations from Luma that meet the criteria of your ruleset.
        /// </summary>
        /// <param name="parameters">Luma parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="LumaInfo"/> object.</returns>
        public async Task<LumaInfo> GetPromise(Parameters.Luma.GetPromise parameters, CancellationToken cancellationToken = default)
        {
            var dict = parameters.ToDictionary();
            return await RequestAsync<LumaInfo>(Method.Post, "luma/promise", cancellationToken, dict, "luma_info");
        }

        #endregion
    }
}
