// Pickup.cs
// See LICENSE for licensing info.

using System;
using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class Pickup : Resource
    {
        public Address address { get; set; }
        public List<CarrierAccount> carrier_accounts { get; set; }
        public string confirmation { get; set; }
        public DateTime? created_at { get; set; }
        public string id { get; set; }
        public string instructions { get; set; }
        public bool is_account_address { get; set; }
        public DateTime max_datetime { get; set; }
        public List<string> messages { get; set; }
        public DateTime min_datetime { get; set; }
        public string mode { get; set; }
        public string name { get; set; }
        public List<Rate> pickup_rates { get; set; }
        public string reference { get; set; }
        public string status { get; set; }
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public void Buy(string carrier, string service)
        {
            Request request = new Request("pickups/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddQueryString(new Dictionary<string, object>
            {
                {
                    "carrier", carrier
                },
                {
                    "service", service
                }
            });

            Merge(request.Execute<Pickup>());
        }

        /// <summary>
        ///     Cancel this pickup.
        /// </summary>
        public void Cancel()
        {
            Request request = new Request("pickups/{id}/cancel", Method.POST);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Pickup>());
        }

        /// <summary>
        ///     Create this Pickup.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Pickup already has an id.</exception>
        public void Create()
        {
            if (id != null)
            {
                throw new ResourceAlreadyCreated();
            }

            Merge(SendCreate(AsDictionary()));
        }

        /// <summary>
        ///     Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///     * {"is_account_address", bool}
        ///     * {"min_datetime", DateTime}
        ///     * {"max_datetime", DateTime}
        ///     * {"reference", string}
        ///     * {"instructions", string}
        ///     * {"carrier_accounts", List&lt;CarrierAccount&gt;}
        ///     * {"address", Address}
        ///     * {"shipment", Shipment}
        ///     * {"batch", Batch}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Create(Dictionary<string, object> parameters = null) => SendCreate(parameters ?? new Dictionary<string, object>());


        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Retrieve(string id)
        {
            Request request = new Request("pickups/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Pickup>();
        }

        private static Pickup SendCreate(Dictionary<string, object> parameters)
        {
            Request request = new Request("pickups", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "pickup", parameters
                }
            });

            return request.Execute<Pickup>();
        }
    }
}
