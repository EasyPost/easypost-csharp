using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    public class Collection : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("filters")]
        internal Dictionary<string, object?>? Filters { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        #endregion

        protected void UpdateFilters(IEnumerable<EasyPostObject>? elements, string propertyName)
        {
            Filters ??= new Dictionary<string, object?>();
            Filters["before_id"] = (elements ?? throw new Exception($"{propertyName} is null")).Last().id ?? throw new Exception("id is null");

            if (Client == null)
            {
                throw new Exception("Client is null");
            }
        }
    }
}
