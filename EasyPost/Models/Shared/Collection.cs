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

        [JsonProperty("has_more")]
        public bool has_more { get; set; }

        [JsonProperty("filters")]
        internal Dictionary<string, object>? filters { get; set; }

        #endregion

        protected void UpdateFilters(IEnumerable<EasyPostObject>? elements, string propertyName)
        {
            filters ??= new Dictionary<string, object>();
            filters["before_id"] = (elements ?? throw new Exception($"{propertyName} is null")).Last().id ?? throw new Exception("id is null");

            if (Client == null)
            {
                throw new Exception("Client is null");
            }
        }
    }
}
