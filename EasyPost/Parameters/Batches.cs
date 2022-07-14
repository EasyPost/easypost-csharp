using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Batches
    {
        public class Create : ApiParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public class UpdateShipments : ApiParameters
        {
            [Parameter(Necessity.Required, "shipments")]
            public List<string>? ShipmentIds { internal get; set; }

            public UpdateShipments(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public class Label : ApiParameters
        {
            [Parameter(Necessity.Required, "file_format")]
            public string? FileFormat { internal get; set; }

            public Label(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
