using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class CustomsItem {
        /// <summary>
        /// Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static async Task<CustomsItem> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a CustomsItem.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the customs item with. Valid pairs:
        ///   * {"description", string}
        ///   * {"quantity", int}
        ///   * {"weight", int}
        ///   * {"value", double}
        ///   * {"hs_tariff_number", string}
        ///   * {"origin_country", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static async Task<CustomsItem> CreateAsync(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }
    }
}