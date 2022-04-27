using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models;

namespace EasyPost.Services
{
    public class CustomsItemService : Service
    {
        internal CustomsItemService(BaseClient client) : base(client)
        {
        }

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
        public async Task<CustomsItem> Create(Dictionary<string, object> parameters)
        {
            return await Create<CustomsItem>("customs_items", parameters);
        }


        /// <summary>
        ///     Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public async Task<CustomsItem> Retrieve(string id)
        {
            return await Get<CustomsItem>($"customs_items/{id}");
        }
    }
}