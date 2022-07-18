using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters;
using EasyPost.Parameters.Beta;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class EndShipperTest : UnitTest
    {
        public EndShipperTest() : base("end_shipper", TestUtils.ApiKey.Production)
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Beta);

            List<EndShipper> endShippers = await Client.EndShippers.All(new All
            {
                PageSize = Fixture.PageSize
            });

            Assert.IsTrue(endShippers.Count <= Fixture.PageSize);
            foreach (EndShipper item in endShippers)
            {
                Assert.IsInstanceOfType(item, typeof(EndShipper));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Beta);

            EndShipper endShipper = await Client.CreateBasicEndShipper();

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.Id.StartsWith("es_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", endShipper.Street1);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Beta);

            EndShipper endShipper = await Client.CreateBasicEndShipper();

            EndShipper retrievedEndShipper = await Client.EndShippers.Retrieve(endShipper.Id);

            Assert.IsInstanceOfType(retrievedEndShipper, typeof(EndShipper));
            Assert.AreEqual(endShipper.Street1, retrievedEndShipper.Street1);
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update", ApiVersion.Beta);

            EndShipper endShipper = await Client.CreateBasicEndShipper();

            const string testName = "NEW NAME";

            endShipper = await endShipper.Update(new EndShippers.Update
            {
                Name = testName,
                Street1 = endShipper.Street1,
                Street2 = endShipper.Street2,
                City = endShipper.City,
                State = endShipper.State,
                Zip = endShipper.Zip,
                Country = endShipper.Country,
                Phone = endShipper.Phone,
                Email = endShipper.Email,
                Company = endShipper.Company
            });

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.Id.StartsWith("es_"));
            Assert.AreEqual(testName, endShipper.Name);
        }
    }
}
