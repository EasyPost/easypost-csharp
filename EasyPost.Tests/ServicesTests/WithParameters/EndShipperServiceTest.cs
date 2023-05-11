using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class EndShipperServiceTests : UnitTest
    {
        public EndShipperServiceTests() : base("end_shipper_service_with_parameters")
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

            Dictionary<string, object> data = Fixtures.CaAddress1;

            Parameters.EndShipper.Create parameters = Fixtures.Parameters.EndShippers.Create(data);

            EndShipper endShipper = await Client.EndShipper.Create(parameters);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", endShipper.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            Parameters.EndShipper.All parameters = Fixtures.Parameters.EndShippers.All(data);

            EndShipperCollection endShipperCollection = await Client.EndShipper.All(parameters);
            List<EndShipper> endShippers = endShipperCollection.EndShippers;

            Assert.True(endShippers.Count <= Fixtures.PageSize);
            foreach (EndShipper item in endShippers)
            {
                Assert.IsType<EndShipper>(item);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            Dictionary<string, object> data = Fixtures.CaAddress1;

            Parameters.EndShipper.Create createParameters = Fixtures.Parameters.EndShippers.Create(data);

            EndShipper endShipper = await Client.EndShipper.Create(createParameters);

            if (IsRecording()) // Give the server time to process the endshipper
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            const string testName = "NEW NAME";

            // Updating an EndShipper requires all the original data to be sent back + the updated data
            Parameters.EndShipper.Update updateParameters = new()
            {
                Name = testName,
                City = createParameters.City,
                Company = createParameters.Company,
                Country = createParameters.Country,
                Email = createParameters.Email,
                Phone = createParameters.Phone,
                State = createParameters.State,
                Street1 = createParameters.Street1,
                Street2 = createParameters.Street2,
                Zip = createParameters.Zip,
            };

            endShipper = await Client.EndShipper.Update(endShipper.Id, updateParameters);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal(testName, endShipper.Name);
        }

        #endregion

        #endregion
    }
}
