using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class CarrierTypeServiceTests : UnitTest
    {
        public CarrierTypeServiceTests() : base("carrier_type_service", TestUtils.ApiKey.Production)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            List<CarrierType> types = await Client.CarrierType.All();

            foreach (CarrierType item in types)
            {
                Assert.IsType<CarrierType>(item);
            }
        }

        #endregion

        #endregion
    }
}
