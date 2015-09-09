using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class CarrierAccount {
        public static async Task<List<CarrierAccount>> ListAsync() {
            return await Task.Run(() => List()).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static async Task<CarrierAccount> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a CarrierAccount.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///   * {"type", string} Required (e.g. EndiciaAccount, UPSAccount, etc.).
        ///   * {"reference", string} External reference for carrier account.
        ///   * {"description", string} Description of carrier account.
        ///   * {"credentials", Dictionary<string, string>}
        ///   * {"test_credentials", Dictionary<string, string>}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static async Task<CarrierAccount> CreateAsync(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove this CarrierAccount from your account.
        /// </summary>
        public async Task DestroyAsync() {
            await Task.Run(() => Destroy()).ConfigureAwait(false);
        }

        /// <summary>
        /// Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        public async Task UpdateAsync(IDictionary<string, object> parameters) {
            await Task.Run(() => Update(parameters)).ConfigureAwait(false);
        }
    }
}