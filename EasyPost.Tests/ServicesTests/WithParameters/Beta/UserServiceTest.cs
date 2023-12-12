using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters.Beta.User;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters.Beta
{
    public class UserServiceTests : UnitTest
    {
        public UserServiceTests() : base("beta_user_service_with_parameters", TestUtils.ApiKey.Production) =>
            CleanupFunction = async id =>
            {
                try
                {
                    User retrievedUser = await Client.User.Retrieve(id);
                    await Client.User.Delete(retrievedUser.Id);
                    return true;
                }
                catch
                {
                    // trying to delete something that doesn't exist, pass
                    return false;
                }
            };

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAllChildren()
        {
            UseVCR("all_children");

            Dictionary<string, object> fixture = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            AllChildren parameters = Fixtures.Parameters.Users.AllChildren(fixture);

            ChildUserCollection childUserCollection = await Client.Beta.User.AllChildren(parameters);
            List<User> children = childUserCollection.Children;

            Assert.True(children.Count <= Fixtures.PageSize);
            foreach (User item in children)
            {
                Assert.IsType<User>(item);
            }
        }

        #endregion

        #endregion
    }
}
