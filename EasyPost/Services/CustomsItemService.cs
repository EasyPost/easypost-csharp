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
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/customs-items">customs item-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomsItemService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomsItemService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal CustomsItemService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="CustomsItem"/>.
        ///     <a href="https://docs.easypost.com/docs/customs-items#create-a-customsitem">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="CustomsItem"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomsItem"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CustomsItem> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("customs_item");
            return await RequestAsync<CustomsItem>(Method.Post, "customs_items", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="CustomsItem"/>.
        ///     <a href="https://docs.easypost.com/docs/customs-items#create-a-customsitem">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="CustomsItem"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomsItem"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CustomsItem> Create(Parameters.CustomsItem.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<CustomsItem>(Method.Post, "customs_items", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a <see cref="CustomsItem"/>.
        ///     <a href="https://docs.easypost.com/docs/customs-items#retrieve-a-customsitem">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="CustomsItem"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomsItem"/> object.</returns>
        [CrudOperations.Read]
        public async Task<CustomsItem> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<CustomsItem>(Method.Get, $"customs_items/{id}", cancellationToken);

        #endregion
    }
}
