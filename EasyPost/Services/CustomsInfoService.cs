using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class CustomsInfoService : EasyPostService
    {
        internal CustomsInfoService(EasyPostClient client) : base(client)
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
        public async Task<CustomsInfo> Create(Dictionary<string, object> parameters)
        {
            return await Create<CustomsInfo>("customs_infos", parameters);
        }

        /// <summary>
        ///     Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        [CrudOperations.Read]
        public async Task<CustomsInfo> Retrieve(string id)
        {
            return await Get<CustomsInfo>($"customs_infos/{id}");
        }

        #endregion
    }
}
