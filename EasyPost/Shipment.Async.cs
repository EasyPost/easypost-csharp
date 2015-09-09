using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Shipment {
        /// <summary>
        /// Get a paginated list of shipments.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} String representing a Shipment. Starts with "shp_". Only retrieve shipments created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a Shipment. Starts with "shp_". Only retrieve shipments created after this id.
        ///   * {"page_size", int} Size of page. Default to 20.
        ///   * {"purchased", bool} If true only display purchased shipments.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <returns>Instance of EasyPost.ShipmentList</returns>
        public static async Task<ShipmentList> ListAsync(Dictionary<string, object> parameters = null) {
            return await Task.Run(() => List(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>EasyPost.Shipment instance.</returns>
        public static async Task<Shipment> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a Shipment.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the shipment with. Valid pairs:
        ///   * {"from_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"to_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"buyer_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"return_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"parcel", Dictionary<string, object>} See Parcel.Create for list of valid keys.
        ///   * {"customs_info", Dictionary<string, object>} See CustomsInfo.Create for lsit of valid keys.
        ///   * {"options", Dictionary<string, object>} See https://www.easypost.com/docs/api#shipments for list of options.
        ///   * {"is_return", bool}
        ///   * {"currency", string} Defaults to "USD".
        ///   * {"reference", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static async Task<Shipment> CreateAsync(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create this Shipment.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Shipment already has an id.</exception>
        public async Task CreateAsync() {
            await Task.Run(() => Create()).ConfigureAwait(false);
        }

        /// <summary>
        /// Populate the rates property for this shipment
        /// </summary>
        public async Task GetRatesAsync() {
            await Task.Run(() => GetRates()).ConfigureAwait(false);
        }

        /// <summary>
        /// Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        public async Task BuyAsync(string rateId) {
            await Task.Run(() => Buy(rateId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object to puchase the shipment with.</param>
        public async Task BuyAsync(Rate rate) {
            await Task.Run(() => Buy(rate)).ConfigureAwait(false);
        }

        /// <summary>
        /// Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        public async Task InsureAsync(double amount) {
            await Task.Run(() => Insure(amount)).ConfigureAwait(false);
        }

        /// <summary>
        /// Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        public async Task GenerateLabelAsync(string fileFormat) {
            await Task.Run(() => GenerateLabel(fileFormat)).ConfigureAwait(false);
        }

        /// <summary>
        /// Generate a stamp for this shipment.
        /// </summary>
        public async Task GenerateStampAsync() {
            await Task.Run(() => GenerateStamp()).ConfigureAwait(false);
        }

        /// <summary>
        /// Generate a barcode for this shipment.
        /// </summary>
        public async Task GenerateBarcodeAsync() {
            await Task.Run(() => GenerateBarcode()).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        public async Task RefundAsync() {
            await Task.Run(() => Refund()).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the lowest rate for the shipment. Optionally whitelist/blacklist carriers and servies from the search.
        /// </summary>
        /// <param name="includeCarriers">Carriers whitelist.</param>
        /// <param name="includeServices">Services whitelist.</param>
        /// <param name="excludeCarriers">Carriers blacklist.</param>
        /// <param name="excludeServices">Services blacklist.</param>
        /// <returns>EasyPost.Rate instance or null if no rate was found.</returns>
        public async Task<Rate> LowestRateAsync(IEnumerable<string> includeCarriers = null,
            IEnumerable<string> includeServices = null,
            IEnumerable<string> excludeCarriers = null, IEnumerable<string> excludeServices = null) {
            return
                await
                    Task.Run(() => LowestRate(includeCarriers, includeServices, excludeCarriers, excludeServices))
                        .ConfigureAwait(false);
        }
    }
}