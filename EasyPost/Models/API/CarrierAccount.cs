using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost._base;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.CarrierAccount
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#carrier-account-object">EasyPost carrier account</a>.
    /// </summary>
    public class CarrierAccount : EasyPostObject, ICarrierAccountParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The type of billing used by the carrier account.
        /// </summary>
        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }

        /// <summary>
        ///     Only the reference and description are possible to update if set to true.
        /// </summary>
        [JsonProperty("clone")]
        public bool? Clone { get; set; }

        /// <summary>
        ///     The raw credential pairs for client library consumption.
        /// </summary>
        [JsonProperty("credentials")]
        public Dictionary<string, object>? Credentials { get; set; }

        /// <summary>
        ///     An optional, human-readable description of the carrier account.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Expanded credential fields.
        /// </summary>
        [JsonProperty("fields")]
        public CarrierFields? Fields { get; set; }

        /// <summary>
        ///     The name used when displaying a readable value for the type of the carrier account.
        /// </summary>
        [JsonProperty("readable")]
        public string? Readable { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The raw test credential pairs for client library consumption.
        /// </summary>
        [JsonProperty("test_credentials")]
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "TestCredentials is the correct name for this property")]
        public Dictionary<string, object>? TestCredentials { get; set; }

        /// <summary>
        ///     The name of the carrier account type.
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="CarrierAccount"/> class.
        /// </summary>
        internal CarrierAccount()
        {
        }
    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.CarrierAccount
