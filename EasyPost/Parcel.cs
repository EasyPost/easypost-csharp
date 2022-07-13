﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Parcel : Resource
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("height")]
        public double? height { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("length")]
        public double? length { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("predefined_package")]
        public string predefined_package { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("weight")]
        public double weight { get; set; }
        [JsonProperty("width")]
        public double? width { get; set; }

        #endregion

        /// <summary>
        ///     Create a Parcel.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the parcel with. Valid pairs:
        ///     * {"length", int}
        ///     * {"width", int}
        ///     * {"height", int}
        ///     * {"weight", double}
        ///     * {"predefined_package", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Parcel instance.</returns>
        public static async Task<Parcel> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("parcels", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "parcel", parameters
                }
            });

            return await request.Execute<Parcel>();
        }


        /// <summary>
        ///     Retrieve a Parcel from its id.
        /// </summary>
        /// <param name="id">String representing a Parcel. Starts with "prcl_".</param>
        /// <returns>EasyPost.Parcel instance.</returns>
        public static async Task<Parcel> Retrieve(string id)
        {
            Request request = new Request("parcels/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Parcel>();
        }
    }
}
