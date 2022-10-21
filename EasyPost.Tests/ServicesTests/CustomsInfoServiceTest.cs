using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class CustomsInfoServiceTests : UnitTest
    {
        public CustomsInfoServiceTests() : base("customs_info_service")
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

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            Assert.IsType<CustomsInfo>(customsInfo);
            Assert.StartsWith("cstinfo_", customsInfo.Id);
            Assert.Equal("NOEEI 30.37(a)", customsInfo.EelPfc);
            Assert.NotEmpty(customsInfo.CustomsItems);
            foreach (CustomsItem item in customsInfo.CustomsItems)
            {
                Assert.StartsWith("cstitem_", item.Id);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            CustomsInfo retrievedCustomsInfo = await Client.CustomsInfo.Retrieve(customsInfo.Id);

            Assert.IsType<CustomsInfo>(retrievedCustomsInfo);
            Assert.Equal(customsInfo, retrievedCustomsInfo);
        }

        #endregion

        #endregion

        private async Task<CustomsInfo> CreateBasicCustomsInfo() => await Client.CustomsInfo.Create(Fixtures.BasicCustomsInfo);
    }
}
