using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class CustomsInfoTest : UnitTest
    {
        public CustomsInfoTest() : base("customs_info")
        {
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            Assert.IsType<CustomsInfo>(customsInfo);
            Assert.StartsWith("cstinfo_", customsInfo.id);
            Assert.Equal("NOEEI 30.37(a)", customsInfo.eel_pfc);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            CustomsInfo retrievedCustomsInfo = await Client.CustomsInfo.Retrieve(customsInfo.id);

            Assert.IsType<CustomsInfo>(retrievedCustomsInfo);
            Assert.Equal(customsInfo, retrievedCustomsInfo);
        }

        private async Task<CustomsInfo> CreateBasicCustomsInfo() => await Client.CustomsInfo.Create(Fixture.BasicCustomsInfo);
    }
}
