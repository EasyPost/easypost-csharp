﻿using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Address : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("carrier_facility")]
        public string? CarrierFacility { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("company")]
        public string? Company { get; set; }
        [JsonProperty("country")]
        public string? Country { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("error")]
        public string? Error { get; set; }
        [JsonProperty("federal_tax_id")]
        public string? FederalTaxId { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("phone")]
        public string? Phone { get; set; }
        [JsonProperty("residential")]
        public bool? Residential { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("state_tax_id")]
        public string? StateTaxId { get; set; }
        [JsonProperty("street1")]
        public string? Street1 { get; set; }
        [JsonProperty("street2")]
        public string? Street2 { get; set; }
        [JsonProperty("verifications")]
        public Verifications? Verifications { get; set; }
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        internal Address()
        {
        }
    }

    public class AddressCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; set; }

        #endregion

        internal AddressCollection()
        {
        }
    }
}
