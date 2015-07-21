using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Address {
        /// <summary>
        /// Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <returns>EasyPost.Address instance.</returns>
        public static async Task<Address> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an Address.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the address with. Valid pairs:
        ///   * {"name", string}
        ///   * {"company", string}
        ///   * {"stree1", string}
        ///   * {"street2", string}
        ///   * {"city", string}
        ///   * {"state", string}
        ///   * {"zip", string}
        ///   * {"country", string}
        ///   * {"phone", string}
        ///   * {"email", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public static async Task<Address> CreateAsync(IDictionary<string, object> parameters = null) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create this Address.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public async Task CreateAsync() {
            await Task.Run(() => Create()).ConfigureAwait(false);
        }

        /// <summary>
        /// Create and verify an Address.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the address with. Valid pairs:
        ///   * {"name", string}
        ///   * {"company", string}
        ///   * {"stree1", string}
        ///   * {"street2", string}
        ///   * {"city", string}
        ///   * {"state", string}
        ///   * {"zip", string}
        ///   * {"country", string}
        ///   * {"phone", string}
        ///   * {"email", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public static async Task<Address> CreateAndVerifyAsync(IDictionary<string, object> parameters = null) {
            return await Task.Run(() => CreateAndVerify(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Verify an address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        public async Task VerifyAsync(string carrier = null) {
            await Task.Run(() => Verify(carrier)).ConfigureAwait(false);
        }
    }
}