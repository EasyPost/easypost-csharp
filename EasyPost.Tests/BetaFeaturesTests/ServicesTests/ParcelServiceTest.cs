using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class ParcelServiceTests : UnitTest
    {
        public ParcelServiceTests() : base("parcel_service")
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

            Dictionary<string, object> data = Fixtures.BasicParcel;

            Parcel parcel = await Client.Parcel.Create(data);

            Assert.IsType<Parcel>(parcel);
            Assert.StartsWith("prcl_", parcel.Id);
            Assert.Equal(15.4, parcel.Weight);
        }

        #endregion

        #endregion
    }
}
