using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class ClaimServiceTests : UnitTest
    {
        public ClaimServiceTests() : base("claim_service_with_parameters")
        {
        }

        #region Tests

        private static async Task<Shipment> PrepareInsuredShipment(Client client, Parameters.Shipment.Create createShipmentParameters, double claimAmount)
        {
            Shipment shipment = await client.Shipment.Create(createShipmentParameters);
            Rate rate = shipment.LowestRate();
            Shipment purchaseShipment = await client.Shipment.Buy(shipment.Id, rate.Id);
            Shipment _ = await client.Shipment.Insure(purchaseShipment.Id, claimAmount);

            return purchaseShipment;
        }

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            const double claimAmount = 100.00;
            Parameters.Shipment.Create createShipmentParameters = Fixtures.Parameters.Shipments.Create(Fixtures.FullShipment);
            Shipment insuredShipment = await PrepareInsuredShipment(Client, createShipmentParameters, claimAmount);

            Dictionary<string, object> claimData = Fixtures.BasicClaim;
            Parameters.Claim.Create claimParameters = Fixtures.Parameters.Claims.Create(claimData);

            claimParameters.TrackingCode = insuredShipment.TrackingCode;
            claimParameters.Amount = claimAmount;

            Claim claim = await Client.Claim.Create(claimParameters);

            Assert.IsType<Claim>(claim);
            Assert.StartsWith("clm_", claim.Id);
            Assert.Equal(claimParameters.TrackingCode, claim.TrackingCode);
            Assert.Equal(claimParameters.Amount, claim.RequestedAmount);
            Assert.Equal(claimParameters.Type, claim.Type);
            Assert.Equal(claim.PaymentMethod, ClaimPaymentMethod.EasyPostWallet);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };
            Parameters.Claim.All parameters = Fixtures.Parameters.Claims.All(data);

            ClaimCollection claimCollection = await Client.Claim.All(parameters);

            List<Claim> claims = claimCollection.Claims;

            Assert.True(claims.Count <= Fixtures.PageSize);
            foreach (Claim item in claims)
            {
                Assert.IsType<Claim>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };
            Parameters.Claim.All parameters = Fixtures.Parameters.Claims.All(data);

            ClaimCollection collection = await Client.Claim.All(parameters);

            try
            {
                ClaimCollection nextPageCollection = await Client.Claim.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Claims[0].Id, nextPageCollection.Claims[0].Id);
            }
            catch (EndOfPaginationError) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            const double claimAmount = 100.00;
            Parameters.Shipment.Create createShipmentParameters = Fixtures.Parameters.Shipments.Create(Fixtures.FullShipment);
            Shipment insuredShipment = await PrepareInsuredShipment(Client, createShipmentParameters, claimAmount);

            Dictionary<string, object> claimData = Fixtures.BasicClaim;
            Parameters.Claim.Create claimParameters = Fixtures.Parameters.Claims.Create(claimData);

            claimParameters.TrackingCode = insuredShipment.TrackingCode;
            claimParameters.Amount = claimAmount;

            Claim claim = await Client.Claim.Create(claimParameters);

            Claim retrievedClaim = await Client.Claim.Retrieve(claim.Id);

            Assert.IsType<Claim>(retrievedClaim);
            // Must compare IDs since other elements of object may be different
            Assert.Equal(claim.Id, retrievedClaim.Id);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            const double claimAmount = 100.00;
            Parameters.Shipment.Create createShipmentParameters = Fixtures.Parameters.Shipments.Create(Fixtures.FullShipment);
            Shipment insuredShipment = await PrepareInsuredShipment(Client, createShipmentParameters, claimAmount);

            Dictionary<string, object> claimData = Fixtures.BasicClaim;
            Parameters.Claim.Create claimParameters = Fixtures.Parameters.Claims.Create(claimData);

            claimParameters.TrackingCode = insuredShipment.TrackingCode;
            claimParameters.Amount = claimAmount;

            Claim claim = await Client.Claim.Create(claimParameters);
            Claim cancelledClaim = await Client.Claim.Cancel(claim.Id);

            Assert.IsType<Claim>(cancelledClaim);
            Assert.StartsWith("clm_", cancelledClaim.Id);
            Assert.Equal("cancelled", cancelledClaim.Status);
        }

        #endregion

        #endregion
    }
}
