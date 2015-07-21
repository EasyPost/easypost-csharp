using System.Threading.Tasks;

namespace EasyPost {
    public partial class ShipmentList {
        /// <summary>
        /// Get the next page of shipments based on the original parameters passed to Shipment.List().
        /// </summary>
        /// <returns>A new EasyPost.ShipmentList instance.</returns>
        public async Task<ShipmentList> NextAsync() {
            return await Task.Run(() => Next()).ConfigureAwait(false);
        }
    }
}