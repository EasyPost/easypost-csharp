﻿using System;
using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class CustomsItem : Resource
    {
        public string code { get; set; }
        public DateTime? created_at { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string hs_tariff_number { get; set; }
        public string id { get; set; }
        public string mode { get; set; }
        public string origin_country { get; set; }
        public int quantity { get; set; }
        public DateTime? updated_at { get; set; }
        public double value { get; set; }
        public double weight { get; set; }

        /// <summary>
        ///     Create a CustomsItem.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the customs item with. Valid pairs:
        ///     * {"description", string}
        ///     * {"quantity", int}
        ///     * {"weight", int}
        ///     * {"value", double}
        ///     * {"hs_tariff_number", string}
        ///     * {"origin_country", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static CustomsItem Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("customs_items", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "customs_item", parameters
                }
            });

            return request.Execute<CustomsItem>();
        }


        /// <summary>
        ///     Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static CustomsItem Retrieve(string id)
        {
            Request request = new Request("customs_items/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<CustomsItem>();
        }
    }
}
