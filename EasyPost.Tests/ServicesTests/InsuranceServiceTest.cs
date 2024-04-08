using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class InsuranceServiceTests : UnitTest
    {
        public InsuranceServiceTests() : base("insurance_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            Dictionary<string, object> parameters = Fixtures.BasicInsurance;
            parameters.Add("tracking_code", shipment.TrackingCode);

            Insurance insurance = await Client.Insurance.Create(parameters);

            Assert.IsType<Insurance>(insurance);
            Assert.StartsWith("ins_", insurance.Id);
            Assert.Equal("100.00000", insurance.Amount);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            InsuranceCollection insuranceCollection = await Client.Insurance.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Insurance> insurances = insuranceCollection.Insurances;

            Assert.True(insurances.Count <= Fixtures.PageSize);
            foreach (Insurance item in insurances)
            {
                Assert.IsType<Insurance>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            InsuranceCollection collection = await Client.Insurance.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                InsuranceCollection nextPageCollection = await Client.Insurance.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Insurances[0].Id, nextPageCollection.Insurances[0].Id);
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

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            Dictionary<string, object> parameters = Fixtures.BasicInsurance;
            parameters.Add("tracking_code", shipment.TrackingCode);

            Insurance insurance = await Client.Insurance.Create(parameters);

            Insurance retrievedInsurance = await Client.Insurance.Retrieve(insurance.Id);
            Assert.IsType<Insurance>(retrievedInsurance);
            // Must compare IDs since other elements of object may be different
            Assert.Equal(insurance.Id, retrievedInsurance.Id);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestRefund()
        {
            UseVCR("refund");

            Dictionary<string, object> parameters = Fixtures.BasicInsurance;
            parameters.Add("tracking_code", "EZ1000000001");

            Insurance insurance = await Client.Insurance.Create(parameters);
            Insurance cancelledInsurance = await Client.Insurance.Refund(insurance.Id);

            Assert.IsType<Insurance>(insurance);
            Assert.StartsWith("ins_", cancelledInsurance.Id);
            Assert.Equal("cancelled", cancelledInsurance.Status);
            Assert.Equal("Insurance was cancelled by the user.", cancelledInsurance.Messages[0]);
        }

        #endregion

        #endregion
    }
}
