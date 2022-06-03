using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class CustomsInfoService : Service
    {
        internal CustomsInfoService(BaseClient client) : base(client)
        {
        }

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
        public async Task<CustomsInfo> Create(Dictionary<string, object> parameters) => await Create<CustomsInfo>("customs_infos", parameters);


        /// <summary>
        ///     Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public async Task<CustomsInfo> Retrieve(string id) => await Get<CustomsInfo>($"customs_infos/{id}");
    }
}