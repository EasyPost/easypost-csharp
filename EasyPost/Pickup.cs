using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Pickup : IResource {
        public string id { get; set; }
        public string mode { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public DateTime min_datetime { get; set; }
        public DateTime max_datetime { get; set; }
        public bool is_account_address { get; set; }
        public string instructions { get; set; }
        public List<string> messages { get; set; }
        public string confirmation { get; set; }
        public Address address { get; set; }
        public List<CarrierAccount> carrier_accounts { get; set; }
        public List<Rate> pickup_rates { get; set; }

        private static Client client = new Client();

        /// <summary>
        /// Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Retrieve(string id) {
            Request request = new Request("pickups/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<Pickup>(request);
        }
        /// <summary>
        /// Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"is_account_address", bool}
        ///   * {"min_datetime", DateTime}
        ///   * {"max_datetime", DateTime}
        ///   * {"reference", string}
        ///   * {"instructions", string}
        ///   * {"carrier_accounts", List<CarrierAccount>}
        ///   * {"address", Address}
        ///   * {"shipment", Shipment}
        ///   * {"batch", Batch}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Create(IDictionary<string, object> parameters = null) {
            return sendCreate(parameters ?? new Dictionary<string, object>());
        }

        /// <summary>
        /// Create this Pickup.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Pickup already has an id.</exception>
        public void Create() {
            if (id != null)
                throw new ResourceAlreadyCreated();
            this.Merge(sendCreate(this.AsDictionary()));
        }

        private static Pickup sendCreate(IDictionary<string, object> parameters) {
            Request request = new Request("pickups", Method.POST);
            request.addBody(parameters, "pickup");

            return client.Execute<Pickup>(request);
        }

        /// <summary>
        /// Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public void Buy(string carrier, string service) {
            Request request = new Request("pickups/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.addBody(new List<Tuple<string, string>>() {
                new Tuple<string, string>("carrier", carrier),
                new Tuple<string, string>("service", service)
            });

            this.Merge(client.Execute<Pickup>(request));
        }

        /// <summary>
        /// Cancel this pickup.
        /// </summary>
        public void Cancel() {
            Request request = new Request("pickups/{id}/cancel", Method.POST);
            request.AddUrlSegment("id", id);

            this.Merge(client.Execute<Pickup>(request));
        }
    }
}
