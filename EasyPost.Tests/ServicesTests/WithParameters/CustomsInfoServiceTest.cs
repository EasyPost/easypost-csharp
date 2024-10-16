using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class CustomsInfoServiceTests : UnitTest
    {
        public CustomsInfoServiceTests() : base("customs_info_service_with_parameters")
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

            Dictionary<string, object> data = Fixtures.BasicCustomsInfo;

            Parameters.CustomsInfo.Create parameters = Fixtures.Parameters.CustomsInfo.Create(data);

            CustomsInfo customsInfo = await Client.CustomsInfo.Create(parameters);

            Assert.IsType<CustomsInfo>(customsInfo);
            Assert.StartsWith("cstinfo_", customsInfo.Id);
            Assert.Equal("NOEEI 30.37(a)", customsInfo.EelPfc);
            foreach (CustomsItem item in customsInfo.CustomsItems)
            {
                Assert.StartsWith("cstitem_", item.Id);
            }
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Exception]
        public async Task TestCreateWithRestrictionAndRestrictionCommentsCombos()
        {
            UseVCR("create_with_restriction_and_restriction_comments_combos");

            Dictionary<string, object> data = Fixtures.BasicCustomsInfo;
            Parameters.CustomsInfo.Create parameters = Fixtures.Parameters.CustomsInfo.Create(data);

            // User must provide comments if restriction is set to anything other than "none"
            parameters.RestrictionType = "other";
            parameters.RestrictionComments = null;

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parameters.ToDictionary());

            parameters.RestrictionType = "other";
            parameters.RestrictionComments = "Explanation";

            CustomAssertions.DoesNotThrow(() => parameters.ToDictionary());

            // User does not necessarily need to provide comments if restriction is set to "none", but can if they want
            parameters.RestrictionType = "none";
            parameters.RestrictionComments = null;

            CustomAssertions.DoesNotThrow(() => parameters.ToDictionary());

            parameters.RestrictionType = "none";
            parameters.RestrictionComments = "Explanation";

            CustomAssertions.DoesNotThrow(() => parameters.ToDictionary());

            // User does not need to provide comments if restriction is not set, but can if they want
            parameters.RestrictionType = null;
            parameters.RestrictionComments = null;

            CustomAssertions.DoesNotThrow(() => parameters.ToDictionary());

            parameters.RestrictionType = null;
            parameters.RestrictionComments = "Explanation";

            CustomAssertions.DoesNotThrow(() => parameters.ToDictionary());
        }

        #endregion

        #endregion
    }
}
