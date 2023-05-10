using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters.Beta.CarrierMetadata;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.ServicesTests.Beta
{
    public class CarrierMetadataServiceTests : UnitTest
    {
        public CarrierMetadataServiceTests() : base("beta_carrier_metadata")
        {
        }

        #region Tests

        #region Test CRUD Operations

        /// <summary>
        ///     Test that we can retrieve all carriers and all metadata from the API when no params are provided.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            List<Carrier> carriers = await Client.Beta.CarrierMetadata.RetrieveCarrierMetadata();

            Assert.IsType<List<Carrier>>(carriers);

            // Assert we get multiple carriers
            CustomAssertions.Any(carriers, carrier => Assert.Equal("usps", carrier.Name));
            CustomAssertions.Any(carriers, carrier => Assert.Equal("fedex", carrier.Name));
        }

        /// <summary>
        ///     Test that we can retrieve all carriers and all metadata from the API when no params are provided.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Parameters]
        public async Task TestRetrieveWithFilters()
        {
            UseVCR("retrieve_with_filters");

            const string carrierName = "usps";

            Retrieve parameters = new()
            {
                Carriers = new List<string> { carrierName },
                Types = new List<CarrierMetadataType> { CarrierMetadataType.ServiceLevels, CarrierMetadataType.PredefinedPackages },
            };

            List<Carrier> carriers = await Client.Beta.CarrierMetadata.RetrieveCarrierMetadata(parameters);

            // Assert we get the single carrier we asked for
            Assert.True(carriers.Count == 1);
            Assert.All(carriers, carrier => Assert.Equal(carrierName, carrier.Name));

            // Assert we get service levels and predefined packages as we asked for, but not supported features or shipment options
            Assert.All(carriers, carrier => Assert.True(carrier.ServiceLevels != null));
            Assert.All(carriers, carrier => Assert.True(carrier.PredefinedPackages != null));
            Assert.All(carriers, carrier => Assert.True(carrier.SupportedFeatures == null));
            Assert.All(carriers, carrier => Assert.True(carrier.ShipmentOptions == null));
        }

        #endregion

        #endregion
    }
}
