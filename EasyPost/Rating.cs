﻿using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class Rating : Resource
    {
        public List<CarrierAccount> carrier_accounts { get; set; }
        public Address from_address { get; set; }
        public List<Parcel> parcels { get; set; }
        public List<object> ratings { get; set; }
        public Address to_address { get; set; }

        /// <summary>
        ///     Create Rating.
        /// </summary>
        /// <param name="parameters">
        ///     dictionary containing parameters to create the shipment with. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"parcels", List&lt;Dictionary&lt;string, object&gt;&gt;} See Parcel.Create for list of valid keys.
        ///     * {"carrier_accounts", List&lt;string&gt;} List of CarrierAccount.id to limit rating.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Rating instance.</returns>
        public static Rating Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("rating/v1/rates", Method.POST);
            request.AddBody(parameters);

            return request.Execute<Rating>();
        }
    }
}
