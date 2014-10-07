using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Container : IResource {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string reference { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double max_weight { get; set; }

        private static Client client = new Client();

        /// <summary>
        /// Retrieve a Container from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Container. Starts with "container_" if passing an id.</param>
        /// <returns>EasyPost.Container instance.</returns>
        public static Container Retrieve(string id) {
            Request request = new Request("containers/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<Container>(request);
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
        public static Container Create(IDictionary<string, object> parameters) {
            Request request = new Request("containers", Method.POST);
            request.addBody(parameters, "container");

            return client.Execute<Container>(request);
        }
    }
}
