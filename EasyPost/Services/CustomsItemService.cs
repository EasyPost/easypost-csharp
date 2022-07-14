using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;

namespace EasyPost.Services
{
    public class CustomsItemService : EasyPostService
    {
        internal CustomsItemService(Client client) : base(client)
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
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<CustomsItem> Create(CustomsItems.Create parameters)
        {
            return await Create<CustomsItem>("customs_items", parameters);
        }


        /// <summary>
        ///     Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<CustomsItem> Retrieve(string id)
        {
            return await Get<CustomsItem>($"customs_items/{id}");
        }
    }
}
