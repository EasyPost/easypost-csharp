﻿using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.User
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#user-object">EasyPost user</a>.
    /// </summary>
    public class User : EasyPostObject, Parameters.IUserParameter
    {
        #region JSON Properties

        /// <summary>
        ///     A list of the <see cref="EasyPost.Models.API.ApiKey"/>s associated with this user.
        /// </summary>
        [JsonProperty("api_keys")]
        public List<ApiKey>? ApiKeys { get; set; }

        /// <summary>
        ///     The user's current account balance.
        ///     Formatted as USD dollars and cents to five decimal places (e.g. "10.00000").
        /// </summary>
        [JsonProperty("balance")]
        public string? Balance { get; set; }

        /// <summary>
        ///     A list of associated child users.
        /// </summary>
        [JsonProperty("children")]
        public List<User>? Children { get; set; }

        /// <summary>
        ///     The user's email address.
        /// </summary>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     The user's full name.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     The ID of the user's parent user, if the user is a child user.
        ///     If the user is a parent user, this field will be null.
        /// </summary>
        [JsonProperty("parent_id")]
        public string? ParentId { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("password_confirmation")]
        public string? PasswordConfirmation { get; set; }

        /// <summary>
        ///     The user's phone number.
        /// </summary>
        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        ///     The average price per shipment for the user's account.
        ///     Formatted as USD dollars and cents to five decimals places (e.g. "10.00000").
        /// </summary>
        [JsonProperty("price_per_shipment")]
        public string? PricePerShipment { get; set; }

        /// <summary>
        ///     Primary recharge amount for the user's balance.
        ///     Formatted as USD dollars and cents, delimited by a decimal point (e.g. "10.00").
        /// </summary>
        [JsonProperty("recharge_amount")]
        public string? RechargeAmount { get; set; }

        /// <summary>
        ///     Number of USD cents that, upon the user's account balance dropping below, trigger an automatic balance recharge using the <see cref="RechargeAmount"/> and <see cref="EasyPost.Models.API.PaymentMethodsSummary.PrimaryPaymentMethod"/>.
        /// </summary>
        [JsonProperty("recharge_threshold")]
        public string? RechargeThreshold { get; set; }

        /// <summary>
        ///     Secondary recharge amount for the user's balance.
        ///     Formatted as USD dollars and cents, delimited by a decimal point (e.g. "10.00").
        /// </summary>
        [JsonProperty("secondary_recharge_amount")]
        public string? SecondaryRechargeAmount { get; set; }

        /// <summary>
        ///     The fee rate for convenience fees.
        ///     Formatted as a decimal percentage (e.g. "0.005").
        /// </summary>
        [JsonProperty("cc_fee_rate")]
        public string? ConvenienceFeeRate { get; set; }

        /// <summary>
        ///     The fee rate for insurance purchases.
        ///     Formatted as a decimal percentage (e.g. "0.005").
        /// </summary>
        [JsonProperty("insurance_fee_rate")]
        public string? InsuranceFeeRate { get; set; }

        /// <summary>
        ///     The minimum cost for insurance purchases.
        ///     Formatted as USD dollars and cents, delimited by a decimal point (e.g. "10.00").
        /// </summary>
        [JsonProperty("insurance_fee_minimum")]
        public string? InsuranceFeeMinimum { get; set; }

        /// <summary>
        ///     Whether or not the child user has been verified.
        /// </summary>
        [JsonProperty("verified")]
        public bool? Verified { get; set; }

        #endregion
    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.User
