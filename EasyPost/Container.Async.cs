using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class Container {
        /// <summary>
        /// Retrieve a Container from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Container. Starts with "container_" if passing an id.</param>
        /// <returns>EasyPost.Container instance.</returns>
        public static async Task<Container> RetrieveAsync(string id) {
            return await Task.Run(() => Retrieve(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a Container.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the container with. Valid pairs:
        ///   * {"name", string}
        ///   * {"type", string}
        ///   * {"reference", string}
        ///   * {"length", double}
        ///   * {"width", double}
        ///   * {"height", double}
        ///   * {"max_weight", double}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Container instance.</returns>
        public static async Task<Container> CreateAsyncTask(IDictionary<string, object> parameters) {
            return await Task.Run(() => Create(parameters)).ConfigureAwait(false);
        }
    }
}