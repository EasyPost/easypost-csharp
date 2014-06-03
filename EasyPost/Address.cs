using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Address {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string mode { get; set; }
        public string error { get; set; }
        public string message { get; set; }

        private static Client client = new Client();

        public static Address Retrieve(string id) {
            Request request = new Request("addresses/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            return client.Execute<Address>(request);
        }

        public static Address Create(string name = null, string company = null, string street1 = null, string street2 = null, string city = null,
                                     string state = null, string zip = null, string country = null, string phone = null, string email = null) {
            Request request = new Request("addresses", Method.POST);
            addPayload(request, name, company, street1, street2, city, state, zip, country, phone, email);
            return client.Execute<Address>(request);
        }

        public static Address CreateAndVerify(string name = null, string company = null, string street1 = null, string street2 = null, string city = null,
                                              string state = null, string zip = null, string country = null, string phone = null, string email = null) {
            Request request = new Request("addresses/create_and_verify", Method.POST);
            request.RootElement = "address";
            addPayload(request, name, company, street1, street2, city, state, zip, country, phone, email);
            return client.Execute<Address>(request);
        }

        public Address Verify() {
            Request request = new Request("addresses/{id}/verify", Method.GET);
            request.RootElement = "address";
            request.AddUrlSegment("id", id);
            return client.Execute<Address>(request);
        }

        private static void addPayload(Request request, string name, string company, string street1, string street2,
                                       string city, string state, string zip, string country, string phone, string email) {
            request.addPayload("address[name]", name);
            request.addPayload("address[company]", company);
            request.addPayload("address[street1]", street1);
            request.addPayload("address[street2]", street2);
            request.addPayload("address[city]", city);
            request.addPayload("address[state]", state);
            request.addPayload("address[zip]", zip);
            request.addPayload("address[country]", country);
            request.addPayload("address[phone]", phone);
            request.addPayload("address[email]", email);
        }
    }
}
