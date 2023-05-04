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
        public string? BillingType { get; internal set; }
        [JsonProperty("credentials")]
        public Dictionary<string, object>? Credentials { get; internal set; }
        [JsonProperty("description")]
        public string? Description { get; internal set; }
        [JsonProperty("readable")]
        public string? Readable { get; internal set; }
        [JsonProperty("reference")]
        public string? Reference { get; internal set; }
        [JsonProperty("test_credentials")]
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "TestCredentials is the correct name for this property")]
        public Dictionary<string, object>? TestCredentials { get; internal set; }
        [JsonProperty("type")]
        public string? Type { get; internal set; }

        #endregion

        internal CarrierAccount()
        {
        }
    }
}
