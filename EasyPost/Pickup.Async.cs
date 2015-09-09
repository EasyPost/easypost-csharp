using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Pickup {
        /// <summary>
        /// Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static async Task<Pickup> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"is_account_address", bool}
        ///   * {"min_datetime", DateTime}
        ///   * {"max_datetime", DateTime}
        ///   * {"reference", string}
        ///   * {"instructions", string}
        ///   * {"carrier_accounts", List<CarrierAccount>}
        ///   * {"address", Address}
        ///   * {"shipment", Shipment}
        ///   * {"batch", Batch}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static async Task<Pickup> CreateAsync(IDictionary<string, object> parameters = null) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create this Pickup.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Pickup already has an id.</exception>
        public async Task CreateAsync() {
            await Task.Run(() => Create()).ConfigureAwait(false);
        }

        /// <summary>
        /// Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public async Task BuyAsync(string carrier, string service) {
            await Task.Run(() => Buy(carrier, service)).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel this pickup.
        /// </summary>
        public async Task CancelAsync() {
            await Task.Run(() => Cancel()).ConfigureAwait(false);
        }
    }
}