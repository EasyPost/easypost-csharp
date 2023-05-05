using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#rates">rate-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RateService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RateService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal RateService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve a <see cref="Rate"/>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Rate"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The retrieved <see cref="Rate"/>.</returns>
        [CrudOperations.Read]
        public async Task<Rate> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Rate>(Method.Get, $"rates/{id}", cancellationToken);

        #endregion
    }
}
