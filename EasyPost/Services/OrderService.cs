using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OrderService : EasyPostService
    {
        internal OrderService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Order.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the order with. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"buyer_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"return_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"customs_info", Dictionary&lt;string, object&gt;} See CustomsInfo.Create for list of valid keys.
        ///     * {"is_return", bool}
        ///     * {"reference", string}
        ///     * {"shipments", IEnumerable&lt;Shipment&gt;} See Shipment.Create for list of valid keys.
        ///     * {"carrier_accounts", IEnumerable&lt;CarrierAccount&gt;}
        ///     * {"containers", IEnumerable&lt;Container&gt;} See Container.Create for list of valid keys.
        ///     * {"items", IEnumerable&lt;Item&gt;} See Item.Create for list of valid keys.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Order instance.</returns>
        [CrudOperations.Create]
        public async Task<Order> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("order");
            return await Create<Order>("orders", parameters);
        }

        /// <summary>
        ///     Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <returns>EasyPost.Order instance.</returns>
        [CrudOperations.Read]
        public async Task<Order> Retrieve(string id)
        {
            return await Get<Order>($"orders/{id}");
        }

        #endregion
    }
}
