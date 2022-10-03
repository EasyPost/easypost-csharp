﻿using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class BatchShipment : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("batch_message")]
        public string? BatchMessage { get; set; }
        [JsonProperty("batch_status")]
        public string? BatchStatus { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion

        internal BatchShipment()
        {
        }
    }
}
