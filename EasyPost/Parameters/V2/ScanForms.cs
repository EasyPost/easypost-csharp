using System.Collections.Generic;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Parameters.V2
{
    public static class ScanForms
    {
        public class Create : EasyPostParameters
        {
            [Parameter("scan_form", "shipments")]
            public List<Shipment>? Shipments { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }
    }
}
