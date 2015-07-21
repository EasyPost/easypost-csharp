using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Batch {
        /// <summary>
        /// Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static async Task<Batch> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a Batch.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"shipments", List<Dictionary<string, object>>} See Shipment.Create for a list of valid keys.
        ///   * {"reference", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static async Task<Batch> CreateAsync(IDictionary<string, object> parameters = null) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        public async Task AddShipmentsAsync(IEnumerable<string> shipmentIds) {
            await Task.Run(() => AddShipments(shipmentIds)).ConfigureAwait(false);
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be added.</param>
        public async Task AddShipmentsAsync(IEnumerable<Shipment> shipments) {
            await Task.Run(() => AddShipments(shipments)).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        public async Task RemoveShipmentsAsync(IEnumerable<string> shipmentIds) {
            await Task.Run(() => RemoveShipments(shipmentIds)).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="shipments">List of Shipment objects to be removed.</param>
        public async Task RemoveShipmentsAsync(IEnumerable<Shipment> shipments) {
            await Task.Run(() => RemoveShipments(shipments)).ConfigureAwait(false);
        }

        /// <summary>
        /// Purchase all shipments within a batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        public async Task BuyAsync() {
            await Task.Run(() => Buy()).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously generate a label containing all of the Shimpent labels belonging to the batch.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="orderBy">Optional parameter to order the generated label. Ex: "reference DESC"</param>
        public async Task GenerateLabelAsync(string fileFormat, string orderBy = null) {
            await Task.Run(() => GenerateLabel(fileFormat, orderBy)).ConfigureAwait(false);
        }

        /// <summary>
        /// Asychronously generate a scan from for the batch.
        /// </summary>
        public async Task GenerateScanFormAsync() {
            await Task.Run(() => GenerateScanForm()).ConfigureAwait(false);
        }
    }
}