using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierAccount : EasyPostObject, ICarrierAccountParameter
    {
        #region JSON Properties

        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }
        [JsonProperty("credentials")]
        public Dictionary<string, object>? Credentials { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("readable")]
        public string? Readable { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("test_credentials")]
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "TestCredentials is the correct name for this property")]
        public Dictionary<string, object>? TestCredentials { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        internal CarrierAccount()
        {
        }
    }
}
