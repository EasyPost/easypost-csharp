using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Utilities;

namespace EasyPost.Parameters
{
    public static class Batches
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "batch")]
            public Models.API.Batch? Batch { internal get; set; }

            #endregion

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
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "shipments")]
            public List<string>? ShipmentIds { internal get; set; }

            #endregion

            public UpdateShipments(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                // TODO: This custom overload does not check for API compatibility.

                // TODO: Please, can we fix this hack in the API?
                Dictionary<string, object> shipmentsDictionary = new Dictionary<string, object>
                {
                };
                ShipmentIds?.Each((shipmentId, index) => { shipmentsDictionary.Add(index.ToString(), shipmentId); });
                return new Dictionary<string, object?>
                {
                    {
                        "shipments", shipmentsDictionary
                    }
                };
            }
        }

        public sealed class CreateDocument : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "file_format")]
            public string? FileFormat { internal get; set; }

            #endregion

            public CreateDocument(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
