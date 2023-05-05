﻿using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost verifications object.
    /// </summary>
    public class Verifications : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("delivery")]
        public Verification? Delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification? Zip4 { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Verifications"/> class.
        /// </summary>
        internal Verifications()
        {
        }
    }
}
