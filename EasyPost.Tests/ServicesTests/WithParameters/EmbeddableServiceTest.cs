using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.Embeddable;
using EasyPost.Parameters.User;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class EmbeddableServiceTests : UnitTest
    {
        public EmbeddableServiceTests() : base("embeddable_service_with_parameters", TestUtils.ApiKey.Production)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestCreateSession()
        {
            UseVCR("create_session");

            Dictionary<string, object> fixture = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };
            AllChildren childrenParameters = Fixtures.Parameters.Users.AllChildren(fixture);
            ChildUserCollection childUserCollection = await Client.User.AllChildren(childrenParameters);

            Parameters.Embeddable.CreateSession parameters = new()
            {
                OriginHost = "https://example.com",
                UserId = childUserCollection.Children[0].Id,
            };
            EmbeddablesSession session = await Client.Embeddable.CreateSession(parameters);

            Assert.IsType<EmbeddablesSession>(session);
        }

        #endregion

        #endregion
    }
}
