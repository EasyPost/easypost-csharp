using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/customs-infos">customs info-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomsInfoService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomsInfoService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal CustomsInfoService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="CustomsInfo"/>.
        ///     <a href="https://docs.easypost.com/docs/customs-infos#create-a-customsinfo">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="CustomsInfo"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomsInfo"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CustomsInfo> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("customs_info");
            return await RequestAsync<CustomsInfo>(Method.Post, "customs_infos", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="CustomsInfo"/>.
        ///     <a href="https://docs.easypost.com/docs/customs-infos#create-a-customsinfo">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="CustomsInfo"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomsInfo"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CustomsInfo> Create(Parameters.CustomsInfo.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<CustomsInfo>(Method.Post, "customs_infos", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a <see cref="CustomsInfo"/>.
        ///     <a href="https://docs.easypost.com/docs/customs-infos#retrieve-a-customsinfo">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CustomsInfo"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomsInfo"/> object.</returns>
        [CrudOperations.Read]
        public async Task<CustomsInfo> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<CustomsInfo>(Method.Get, $"customs_infos/{id}", cancellationToken);

        #endregion
    }
}
