using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using EasyPost.Models.Beta;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ReferralTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("referral");
        }

        private static async Task<Referral> CreateReferralUser(BetaClient client)
        {
            return await client.Partners.CreateReferral(new Dictionary<string, object>
            {
                {
                    "name", "Test Referral"
                },
                {
                    "email", "me@email.com"
                },
                {
                    "phone", "1234567890"
                }
            });
        }


        [TestMethod]
        public async Task TestCreate()
        {
            BetaClient client = (BetaClient)_vcr.SetUpTest("create", null, ClientVersion.Beta);

            Referral referral = await CreateReferralUser(client);

            Assert.IsNotNull(referral);
            Assert.IsInstanceOfType(referral, typeof(Referral));
            Assert.AreEqual("Test Referral", referral.name);

        }
    }
}
