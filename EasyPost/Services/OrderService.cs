using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#orders">order-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OrderService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal OrderService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an <see cref="Order"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-order">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Order"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Order"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Order> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("order");
            return await RequestAsync<Order>(Method.Post, "orders", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create an <see cref="Order"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-order">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Order"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Order"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Order> Create(BetaFeatures.Parameters.Orders.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Order>(Method.Post, "orders", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Order instance.</returns>
        [CrudOperations.Read]
        public async Task<Order> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Order>(Method.Get, $"orders/{id}", cancellationToken);

        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="withCarrier">The carrier to purchase a shipment from.</param>
        /// <param name="withService">The service to purchase.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Order.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string id, string withCarrier, string withService, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "carrier", withCarrier },
                { "service", withService },
            };

            return await RequestAsync<Order>(Method.Post, $"orders/{id}/buy", cancellationToken, parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated Order.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string id, Rate rate, CancellationToken cancellationToken = default)
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

            return await Buy(id, rate.Carrier, rate.Service, cancellationToken);
        }

        /// <summary>
        ///     Purchase the <see cref="Shipments"/> within this <see cref="Order"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Orders.Buy"/> parameters set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Order"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string id, BetaFeatures.Parameters.Orders.Buy parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Order>(Method.Post, $"orders/{id}/buy", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Repopulate the rates for an Order.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The task to refresh this Order's rates.</returns>
        [CrudOperations.Update]
        public async Task<Order> RefreshRates(string id, CancellationToken cancellationToken = default)
        {
            // TODO: Make consistent with Shipment, Pickup and Order: GetRates, RefreshRates, RegenerateRates?
            return await RequestAsync<Order>(Method.Get, $"orders/{id}/rates", cancellationToken);
        }

        #endregion
    }
}
