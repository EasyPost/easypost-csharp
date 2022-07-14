using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Batches
    {
        public sealed class Create : RequestParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class UpdateShipments : RequestParameters
        {
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "shipments")]
            public List<string>? ShipmentIds { internal get; set; }

            public UpdateShipments(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Label : RequestParameters
        {
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "file_format")]
            public string? FileFormat { internal get; set; }

            public Label(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
