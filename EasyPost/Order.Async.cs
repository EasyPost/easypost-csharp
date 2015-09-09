using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Order {
        /// <summary>
        /// Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <returns>EasyPost.Order instance.</returns>
        public static async Task<Order> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a Order.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the order with. Valid pairs:
        ///   * {"from_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"to_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"buyer_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"return_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"customs_info", Dictionary<string, object>} See CustomsInfo.Create for list of valid keys.
        ///   * {"options", Dictionary<string, object>} See https://www.easypost.com/docs/api#shipments for list of options.
        ///   * {"is_return", bool}
        ///   * {"reference", string}
        ///   * {"shipments", IEnumerable<Shipment>} See Shipment.Create for list of valid keys.
        ///   * {"containers", IEnumerable<Container>} See Container.Create for list of valid keys.
        ///   * {"items", IEnumerable<Item>} See Item.Create for list of valid keys.
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Order instance.</returns>
        public static async Task<Order> CreateAsync(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create this Order.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Order already has an id.</exception>
        public async Task CreateAsync() {
            await Task.Run(() => Create()).ConfigureAwait(false);
        }

        /// <summary>
        /// Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="carrier">The carrier to purchase a shipment from.</param>
        /// <param name="service">The service to purchase.</param>
        public async Task BuyAsync(string carrier, string service) {
            await Task.Run(() => Buy(carrier, service)).ConfigureAwait(false);
        }

        /// <summary>
        /// Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object to puchase the shipment with.</param>
        public async Task BuyAsync(Rate rate) {
            await Task.Run(() => Buy(rate)).ConfigureAwait(false);
        }
    }
}