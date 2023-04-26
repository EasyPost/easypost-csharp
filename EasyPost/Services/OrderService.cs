using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OrderService : EasyPostService
    {
        internal OrderService(EasyPostClient client)
            : base(client)
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
            return await Request<Order>(Method.Post, "orders", parameters);
        }

        /// <summary>
        ///     Create an <see cref="Order"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Orders.Create"/> parameter set.</param>
        /// <returns><see cref="Order"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Order> Create(BetaFeatures.Parameters.Orders.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<Order>(Method.Post,"orders", parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <returns>EasyPost.Order instance.</returns>
        [CrudOperations.Read]
        public async Task<Order> Retrieve(string id) => await Request<Order>(Method.Get, $"orders/{id}");
        
        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="withCarrier">The carrier to purchase a shipment from.</param>
        /// <param name="withService">The service to purchase.</param>
        /// <returns>The updated Order.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string id, string withCarrier, string withService)
        {
            Dictionary<string, object> parameters = new()
            {
                { "carrier", withCarrier },
                { "service", withService },
            };

            return await Request<Order>(Method.Post, $"orders/{id}/buy", parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        /// <returns>The updated Order.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string id, Rate rate)
        {
            if (rate.Carrier == null)
            {
                throw new MissingPropertyError(rate, nameof(rate.Carrier));
            }

#pragma warning disable IDE0046
            if (rate.Service == null)
#pragma warning restore IDE0046
            {
                throw new MissingPropertyError(rate, nameof(rate.Service));
            }

            return await Buy(id, rate.Carrier, rate.Service);
        }

        /// <summary>
        ///     Purchase the <see cref="Shipments"/> within this <see cref="Order"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Orders.Buy"/> parameters set.</param>
        /// <returns>This updated <see cref="Order"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string id, BetaFeatures.Parameters.Orders.Buy parameters)
        {
            return await Request<Order>(Method.Post, $"orders/{id}/buy", parameters.ToDictionary());
        }

        /// <summary>
        ///     Repopulate the rates for an Order.
        /// </summary>
        /// <returns>The task to refresh this Order's rates.</returns>
        [CrudOperations.Update]
        public async Task<Order> RefreshRates(string id)
        {
            // TODO: Make consistent with Shipment, Pickup and Order: GetRates, RefreshRates, RegenerateRates?
            return await Request<Order>(Method.Get, $"orders/{id}/rates");
        }

        #endregion
    }
}
