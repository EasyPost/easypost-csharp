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
    public class CustomsInfoService : EasyPostService
    {
        internal CustomsInfoService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a CustomsInfo.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the customs info with. Valid pairs:
        ///     * {"customs_certify", bool}
        ///     * {"customs_signer", string}
        ///     * {"contents_type", string}
        ///     * {"contents_explanation", string}
        ///     * {"restriction_type", string}
        ///     * {"eel_pfc", string}
        ///     * {"custom_items", Dictionary&lt;string, object&gt;} -- Can contain the key "id" or all keys required to create a
        ///     CustomsItem.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        [CrudOperations.Create]
        public async Task<CustomsInfo> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("customs_info");
            return await RequestAsync<CustomsInfo>(Method.Post, "customs_infos", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="CustomsInfo"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.CustomsInfo.Create"/> parameter set.</param>
        /// <returns><see cref="CustomsInfo"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<CustomsInfo> Create(BetaFeatures.Parameters.CustomsInfo.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<CustomsInfo>(Method.Post, "customs_infos", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        [CrudOperations.Read]
        public async Task<CustomsInfo> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<CustomsInfo>(Method.Get, $"customs_infos/{id}", cancellationToken);

        #endregion
    }
}
