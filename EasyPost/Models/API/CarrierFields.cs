using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost <see cref="CarrierAccount"/>'s <a href="https://www.easypost.com/docs/api#carrier-account-fields-object">credentials</a>.
    /// </summary>
    public class CarrierFields : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The credentials used in the production environment.
        /// </summary>
        [JsonProperty("credentials")]
        public Dictionary<string, CredentialsField>? Credentials { get; set; }

        /// <summary>
        ///     The credentials used in the test environment.
        /// </summary>
        [JsonProperty("test_credentials")]
#pragma warning disable SA1515
        // ReSharper disable once InconsistentNaming
#pragma warning restore SA1515
        public Dictionary<string, CredentialsField>? TestCredentials { get; set; }

        /// <summary>
        ///     For USPS, this designates that no credentials are required.
        /// </summary>
        [JsonProperty("auto_link")]
        public bool? AutoLink { get; set; }

        /// <summary>
        ///     When true, a separate authentication process will be required through the UI to link this account type.
        /// </summary>
        [JsonProperty("custom_workflow")]
        public bool? CustomWorkflow { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class representing a single EasyPost <see cref="CarrierAccount"/>'s credentials entry details.
    /// </summary>
    public class CredentialsField : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The visibility value is used to control form field types.
        ///     See <see cref="CarrierType"/> for more details.
        /// </summary>
        [JsonProperty("visibility")]
        public string? Visibility { get; set; }

        /// <summary>
        ///     The label value is used in form rendering to display a more precise name for the field.
        ///     Possible values include: "visible", "checkbox", "fake", "password" and "masked".
        /// </summary>
        [JsonProperty("label")]
        public string? Label { get; set; }

        /// <summary>
        ///     The value of the field.
        ///     Checkbox fields use "0" and "1" as false and true, respectively.
        ///     All other field types present plaintext, partially-masked or fully-masked credential data for reference.
        /// </summary>
        [JsonProperty("value")]
        public string? Value { get; set; }

        #endregion
    }
}
