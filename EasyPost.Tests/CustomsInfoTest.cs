using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
            UseVCR("create", ApiVersion.Latest);

            CustomsInfo customsInfo = await Client.CreateBasicCustomsInfo();

            Assert.IsInstanceOfType(customsInfo, typeof(CustomsInfo));
            Assert.IsTrue(customsInfo.Id.StartsWith("cstinfo_"));
            Assert.AreEqual("NOEEI 30.37(a)", customsInfo.EelPfc);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            CustomsInfo customsInfo = await Client.CreateBasicCustomsInfo();

            CustomsInfo retrievedCustomsInfo = await Client.CustomsInfo.Retrieve(customsInfo.Id);

            Assert.IsInstanceOfType(retrievedCustomsInfo, typeof(CustomsInfo));
            Assert.AreEqual(customsInfo, retrievedCustomsInfo);
        }
    }
}
