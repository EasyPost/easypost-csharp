using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class CustomsItemServiceTests : UnitTest
    {
        public CustomsItemServiceTests() : base("customs_item_service")
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

            Dictionary<string, object> data = Fixtures.BasicCustomsItem;

            BetaFeatures.Parameters.CustomsItems.Create parameters = Fixtures.Parameters.CustomsItems.Create(data);

            CustomsItem customsItem = await Client.CustomsItem.Create(parameters);

            Assert.IsType<CustomsItem>(customsItem);
            Assert.StartsWith("cstitem_", customsItem.Id);
            Assert.Equal(23.25, customsItem.Value);
        }

        #endregion

        #endregion
    }
}
