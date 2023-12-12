using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.Beta
{
    public class UserServiceTests : UnitTest
    {
        public UserServiceTests() : base("beta_user_service", TestUtils.ApiKey.Production) =>
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

            ChildUserCollection childUserCollection = await Client.Beta.User.AllChildren(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            List<User> children = childUserCollection.Children;

            Assert.True(children.Count <= Fixtures.PageSize);
            foreach (User item in children)
            {
                Assert.IsType<User>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPageOfChildren()
        {
            UseVCR("get_next_page_of_children");

            ChildUserCollection collection = await Client.Beta.User.AllChildren(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                ChildUserCollection nextPageCollection = await Client.Beta.User.GetNextPageOfChildren(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Children[0].Id, nextPageCollection.Children[0].Id);
            }
            catch (EndOfPaginationError) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }

        #endregion

        #endregion
    }
}
