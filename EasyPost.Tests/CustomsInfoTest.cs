﻿using System.Threading.Tasks;
using EasyPost.Models.V2;
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
            UseVCR("create");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            Assert.IsInstanceOfType(customsInfo, typeof(CustomsInfo));
            Assert.IsTrue(customsInfo.id.StartsWith("cstinfo_"));
            Assert.AreEqual("NOEEI 30.37(a)", customsInfo.eel_pfc);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo();

            CustomsInfo retrievedCustomsInfo = await V2Client.CustomsInfo.Retrieve(customsInfo.id);

            Assert.IsInstanceOfType(retrievedCustomsInfo, typeof(CustomsInfo));
            Assert.AreEqual(customsInfo, retrievedCustomsInfo);
        }

        private async Task<CustomsInfo> CreateBasicCustomsInfo() => await V2Client.CustomsInfo.Create(Fixture.BasicCustomsInfo);
    }
}
