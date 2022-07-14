using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.API;

namespace EasyPost.Parameters
{
    public static class ScanForms
    {
        public sealed class Create : ApiParameters
        {
            [Parameter(Necessity.Required, "scan_form", "shipments")]
            public List<Shipment>? Shipments { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
