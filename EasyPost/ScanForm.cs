using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class ScanForm {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<string> tracking_codes { get; set; }
        public Address address { get; set; }
        public string form_url { get; set; }
        public string form_file_type { get; set; }
        public string mode { get; set; }

        private static Client client = new Client();

        /// <summary>
        /// Retrieve a ScanForm from its id.
        /// </summary>
        /// <param name="id">String representing a ScanForm. Starts with "sf_".</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Retrieve(string id) {
            Request request = new Request("scan_forms/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<ScanForm>(request);
        }

        /// <summary>
        /// Create a ScanFrom for the given tracking codes.
        /// </summary>
        /// <param name="trackingCodes">List of tracking codes.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Create(List<string> trackingCodes) {
            Request request = new Request("scan_forms", Method.POST);
            request.addBody(new List<Tuple<string, string>>() {
                new Tuple<string, string>("scan_form[tracking_codes]", string.Join(",", trackingCodes))
            });

            return client.Execute<ScanForm>(request);
        }

        /// <summary>
        /// Create a ScanForm for the given shipments.
        /// </summary>
        /// <param name="shipments">List of Shipment objects.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Create(List<Shipment> shipments) {
            return Create(shipments.Select(shipment => shipment.tracking_code).ToList());
        }
    }
}
