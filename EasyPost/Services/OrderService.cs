using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services;

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
        return await Request<Order>(Method.Post, "orders", parameters);
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
    [CrudOperations.Update]
    public async Task<Order> Buy(string id, string withCarrier, string withService)
    {
        Dictionary<string, object> parameters = new()
        {
            { "carrier", withCarrier },
            { "service", withService }
        };

        return await Request<Order>(Method.Post, $"orders/{id}/buy", parameters);
    }

    /// <summary>
    ///     Purchase a label for this shipment with the given rate.
    /// </summary>
    /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
    [CrudOperations.Update]
    public async Task<Order> Buy(string id, Rate rate)
    {
        if (rate.Carrier == null)
        {
            throw new MissingPropertyError(rate, "Carrier");
        }

        if (rate.Service == null)
        {
            throw new MissingPropertyError(rate, "Service");
        }

        return await Buy(id, rate.Carrier, rate.Service);
    }

    /// <summary>
    ///     Populate the rates property for this Order.
    /// </summary>
    [CrudOperations.Update]
    public async Task<Order> GetRates(string id) => await Request<Order>(Method.Get, $"orders/{id}/rates");

    #endregion

    /// <summary>
    ///     Get the lowest rate for this Order.
    /// </summary>
    /// <param name="includeCarriers">Carriers to include in the filter.</param>
    /// <param name="includeServices">Services to include in the filter.</param>
    /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
    /// <param name="excludeServices">Services to exclude in the filter.</param>
    /// <returns>Lowest EasyPost.Rate object instance.</returns>
    public Rate LowestRate(IEnumerable<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => Calculation.Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);

    /// <summary>
    ///     Get the lowest rate for this Order.
    /// </summary>
    /// <param name="includeCarriers">Carriers to include in the filter.</param>
    /// <param name="includeServices">Services to include in the filter.</param>
    /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
    /// <param name="excludeServices">Services to exclude in the filter.</param>
    /// <returns>Lowest EasyPost.Rate object instance.</returns>
    public Rate LowestRate(Order order, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
    {
        if (order.Rates == null)
        {
            throw new MissingPropertyError(order, "Rates");
        }

        return LowestRate(order.Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
    }
}
