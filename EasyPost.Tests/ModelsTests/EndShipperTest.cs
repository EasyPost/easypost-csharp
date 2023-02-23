using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class EndShipperTests : UnitTest
    {
        public EndShipperTests() : base("end_shipper")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            EndShipper endShipper = await Client.EndShipper.Create(Fixtures.CaAddress1);

            const string testName = "NEW NAME";

            Dictionary<string, object> endShipperData = Fixtures.CaAddress1;
            endShipperData["name"] = testName;

            endShipper = await endShipper.Update(endShipperData);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal(testName, endShipper.Name);
        }

        #endregion

        #endregion
    }
}
