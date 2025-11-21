using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.CustomerPortal;
using EasyPost.Parameters.User;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class CustomerPortalServiceTests : UnitTest
    {
        public CustomerPortalServiceTests() : base("customer_portal_service_with_parameters", TestUtils.ApiKey.Production)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestCreateAccountLink()
        {
            UseVCR("create_account_link");

            Dictionary<string, object> fixture = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };
            AllChildren childrenParameters = Fixtures.Parameters.Users.AllChildren(fixture);
            ChildUserCollection childUserCollection = await Client.User.AllChildren(childrenParameters);

            Parameters.CustomerPortal.CreateAccountLink parameters = new()
            {
                SessionType = "account_onboarding",
                UserId = childUserCollection.Children[0].Id,
                RefreshUrl = "https://example.com/refresh",
                ReturnUrl = "https://example.com/return",
            };
            CustomerPortalAccountLink accountLink = await Client.CustomerPortal.CreateAccountLink(parameters);

            Assert.IsType<CustomerPortalAccountLink>(accountLink);
        }

        #endregion

        #endregion
    }
}
