using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Item {
        /// <summary>
        /// Retrieve an Item from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Item. Starts with "item_" if passing an id.</param>
        /// <returns>EasyPost.Item instance.</returns>
        public static async Task<Item> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an Item.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the item with. Valid pairs:
        ///   * {"name", string}
        ///   * {"description", string}
        ///   * {"reference", string}
        ///   * {"harmonized_code", string}
        ///   * {"country_of_origin", string}
        ///   * {"warehouse_location", string}
        ///   * {"value", double}
        ///   * {"length", double}
        ///   * {"width", double}
        ///   * {"height", double}
        ///   * {"weight", double}
        ///   ADD ANY CUSTOM REFERENCES HERE
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Item instance.</returns>
        public static async Task<Item> CreateAsync(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a Item from a custom reference.
        /// </summary>
        /// <param name="name">String containing the name of the custom reference to search for.</param>
        /// <param name="value">String containing the value of the custom reference to search for.</param>
        /// <returns>EasyPost.Item instance.</returns>
        public static async Task<Item> RetrieveReferenceAsync(string name, string value) {
            return await Task.Run(() => RetrieveReference(name, value)).ConfigureAwait(false);
        }
    }
}