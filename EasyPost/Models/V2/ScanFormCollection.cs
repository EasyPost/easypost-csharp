﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ScanFormCollection : Collection
    {
        [JsonProperty("scan_forms")]
        public List<ScanForm>? scan_forms { get; set; }

        public V2Client? V2Client { get; set; } // override the BaseClient property with a client property

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        public async Task<ScanFormCollection> Next()
        {
            filters ??= new Dictionary<string, object>();
            filters["before_id"] = (scan_forms ?? throw new PropertyMissing("scan_forms")).Last().id ?? throw new PropertyMissing("id");

            if (V2Client == null)
            {
                throw new Exception("Client is null");
            }

            return await V2Client.ScanForms.All(filters);
        }
    }
}
