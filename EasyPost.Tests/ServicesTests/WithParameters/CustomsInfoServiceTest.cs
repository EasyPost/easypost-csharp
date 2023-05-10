using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.CustomsInfo;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class CustomsInfoServiceTests : UnitTest
    {
        public CustomsInfoServiceTests() : base("customs_info_service_with_parameters")
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

            Dictionary<string, object> data = Fixtures.BasicCustomsInfo;

            Create parameters = Fixtures.Parameters.CustomsInfo.Create(data);

            CustomsInfo customsInfo = await Client.CustomsInfo.Create(parameters);

            Assert.IsType<CustomsInfo>(customsInfo);
            Assert.StartsWith("cstinfo_", customsInfo.Id);
            Assert.Equal("NOEEI 30.37(a)", customsInfo.EelPfc);
            foreach (CustomsItem item in customsInfo.CustomsItems)
            {
                Assert.StartsWith("cstitem_", item.Id);
            }
        }

        #endregion

        #endregion
    }
}
