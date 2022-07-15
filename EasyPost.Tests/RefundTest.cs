using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class RefundTest : UnitTest
    {
        public RefundTest() : base("refund")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            RefundCollection refundCollection = await Client.Refunds.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<Refund> refunds = refundCollection.Refunds;

            Assert.IsTrue(refunds.Count <= Fixture.PageSize);
            Assert.IsNotNull(refundCollection.HasMore);
            foreach (Refund item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            List<Refund> refunds = await Client.CreateBasicRefund();

            foreach (Refund item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }

            Refund refund = refunds[0];
            Assert.IsTrue(refund.Id.StartsWith("rfnd_"));
            Assert.AreEqual("submitted", refund.Status);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            RefundCollection refundCollection = await Client.Refunds.All(new All
            {
                PageSize = Fixture.PageSize
            });

            Refund refund = (await Client.CreateBasicRefund())[0];

            Refund retrievedRefund = await Client.Refunds.Retrieve(refund.Id);

            Assert.IsInstanceOfType(retrievedRefund, typeof(Refund));
            Assert.AreEqual(refund, retrievedRefund);
        }
    }
}
