using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Container : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string reference { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double max_weight { get; set; }

        /// <summary>
        /// Retrieve a Container from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Container. Starts with "container_" if passing an id.</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Container instance.</returns>
        public static Container Retrieve(string id, string apiKey = null) {
            Request request = new Request("containers/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Container>(apiKey);
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
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Container instance.</returns>
        public static Container Create(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("containers", Method.POST);
            request.AddBody(parameters, "container");

            return request.Execute<Container>(apiKey);
        }
    }
}
