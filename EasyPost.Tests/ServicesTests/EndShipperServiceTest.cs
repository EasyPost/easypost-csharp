using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class EndShipperServiceTests : UnitTest
    {
        public EndShipperServiceTests() : base("end_shipper_service")
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

            EndShipper endShipper = await Client.EndShipper.Create(Fixtures.CaAddress1);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", endShipper.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            EndShipperCollection endShipperCollection = await Client.EndShipper.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            List<EndShipper> endShippers = endShipperCollection.EndShippers;

            Assert.True(endShippers.Count <= Fixtures.PageSize);
            foreach (EndShipper item in endShippers)
            {
                Assert.IsType<EndShipper>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            EndShipper endShipper = await Client.EndShipper.Create(Fixtures.CaAddress1);

            EndShipper retrievedEndShipper = await Client.EndShipper.Retrieve(endShipper.Id);

            Assert.IsType<EndShipper>(retrievedEndShipper);
            Assert.Equal(endShipper.Street1, retrievedEndShipper.Street1);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            EndShipper endShipper = await Client.EndShipper.Create(Fixtures.CaAddress1);

            if (IsRecording()) // Give the server time to process the endshipper
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            const string testName = "NEW NAME";

            Dictionary<string, object> endShipperData = Fixtures.CaAddress1;
            endShipperData["name"] = testName;

            endShipper = await Client.EndShipper.Update(endShipper.Id, endShipperData);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal(testName, endShipper.Name);
        }

        #endregion

        #endregion
    }
}
