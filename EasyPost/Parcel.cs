using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Parcel : IResource {
        public string id { get; set; }
        public string mode { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public Nullable<double> length { get; set; }
        public Nullable<double> width { get; set; }
        public Nullable<double> height { get; set; }
        public double weight { get; set; }
        public string predefined_package { get; set; }

        /// <summary>
        /// Retrieve a Parcel from its id.
        /// </summary>
        /// <param name="id">String representing a Parcel. Starts with "prcl_".</param>
        /// <returns>EasyPost.Parcel instance.</returns>
        public static Parcel Retrieve(string id) {
            Request request = new Request("parcels/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Parcel>();
        }

        /// <summary>
        /// Create a Parcel.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the parcel with. Valid pairs:
        ///   * {"length", int}
        ///   * {"width", int}
        ///   * {"height", int}
        ///   * {"weight", double}
        ///   * {"predefined_package", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Parcel instance.</returns>
        public static Parcel Create(Dictionary<string, object> parameters) {
            Request request = new Request("parcels", Method.POST);
            request.AddBody(parameters, "parcel");

            return request.Execute<Parcel>();
        }
    }
}
