using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class UserServiceTests : UnitTest
    {
        public UserServiceTests() : base("user_service", TestUtils.ApiKey.Production) =>
            CleanupFunction = async id =>
            {
                try
                {
                    User retrievedUser = await Client.User.Retrieve(id);
                    await retrievedUser.Delete();
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
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateChild()
        {
            UseVCR("create_child");

            User user = await CreateChildUser();

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal("Test User", user.Name);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            User authenticatedUser = await RetrieveMe();

            string id = authenticatedUser.Id;

            User user = await Client.User.Retrieve(id);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal(id, user.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieveWithNoId()
        {
            UseVCR("retrieve_with_no_id");

            User authenticatedUser = await RetrieveMe();

            User user = await Client.User.Retrieve();
            // retrieve with no id should return the authenticated user

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal(authenticatedUser.Id, user.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieveMe()
        {
            UseVCR("retrieve_me");

            User user = await RetrieveMe();

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
        }

        #endregion

        #endregion

        private async Task<User> CreateChildUser()
        {
            User user = await Client.User.CreateChild(new Dictionary<string, object> { { "name", "Test User" } });
            CleanUpAfterTest(user.Id);

            return user;
        }

        private async Task<User> RetrieveMe() => await Client.User.RetrieveMe();
    }
}
