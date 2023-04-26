using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
#pragma warning disable xUnit1004
    public class EndShipperTests : UnitTest
    {
        public EndShipperTests() : base("end_shipper_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            Dictionary<string, object> data = Fixtures.CaAddress1;

            BetaFeatures.Parameters.EndShippers.Create createParameters = Fixtures.Parameters.EndShippers.Create(data);

            EndShipper endShipper = await Client.EndShipper.Create(createParameters);

            const string testName = "NEW NAME";

            // Updating an EndShipper requires all the original data to be sent back + the updated data
            BetaFeatures.Parameters.EndShippers.Update updateParameters = new BetaFeatures.Parameters.EndShippers.Update
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

            endShipper = await endShipper.Update(updateParameters);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal(testName, endShipper.Name);
        }

        #endregion

        #endregion
    }
}
