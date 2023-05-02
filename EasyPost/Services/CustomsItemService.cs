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
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomsItemService : EasyPostService
    {
        internal CustomsItemService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a CustomsItem.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the customs item with. Valid pairs:
        ///     * {"description", string}
        ///     * {"quantity", int}
        ///     * {"weight", int}
        ///     * {"value", double}
        ///     * {"hs_tariff_number", string}
        ///     * {"origin_country", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        [CrudOperations.Create]
        public async Task<CustomsItem> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("customs_item");
            return await RequestAsync<CustomsItem>(Method.Post, "customs_items", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="CustomsItem"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.CustomsItems.Create"/> parameter set.</param>
        /// <returns><see cref="CustomsItem"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<CustomsItem> Create(BetaFeatures.Parameters.CustomsItems.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<CustomsItem>(Method.Post, "customs_items", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        [CrudOperations.Read]
        public async Task<CustomsItem> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<CustomsItem>(Method.Get, $"customs_items/{id}", cancellationToken);

        #endregion
    }
}
