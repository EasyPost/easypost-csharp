using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class CustomsInfoTest : UnitTest
    {
        public CustomsInfoTest() : base("customs_info")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            Assert.IsType<CustomsInfo>(customsInfo);
            Assert.StartsWith("cstinfo_", customsInfo.Id);
            Assert.Equal("NOEEI 30.37(a)", customsInfo.EelPfc);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            CustomsInfo retrievedCustomsInfo = await Client.CustomsInfo.Retrieve(customsInfo.Id);

            Assert.IsType<CustomsInfo>(retrievedCustomsInfo);
            Assert.Equal(customsInfo, retrievedCustomsInfo);
        }

        #endregion

        private async Task<CustomsInfo> CreateBasicCustomsInfo() => await Client.CustomsInfo.Create(Fixtures.BasicCustomsInfo);
    }
}
