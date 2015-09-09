using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class CustomsInfo {
        /// <summary>
        /// Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public static async Task<CustomsInfo> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a CustomsInfo.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the customs info with. Valid pairs:
        ///   * {"customs_certify", bool}
        ///   * {"customs_signer", string}
        ///   * {"contents_type", string}
        ///   * {"contents_explanation", string}
        ///   * {"restriction_type", string}
        ///   * {"eel_pfc", string}
        ///   * {"custom_items", Dictionary<string, object>} -- Can contain the key "id" or all keys required to create a CustomsItem.
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public static async Task<CustomsInfo> CreateAsync(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }
    }
}