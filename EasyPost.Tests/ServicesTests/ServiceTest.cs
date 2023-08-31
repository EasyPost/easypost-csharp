using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    /// <summary>
    ///     This class is for unit tests for basic service functionality, not specific to any one sub-service.
    /// </summary>
    public class ServiceTests : UnitTest
    {
        public ServiceTests() : base("base_service")
        {
        }

        /// <summary>
        ///     This test confirms that the GetNextPage method works as expected.
        ///     This method is implemented on a per-service, but rather than testing in each service, we'll test it once here.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            AddressCollection addressCollection = await Client.Address.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            string firstId = addressCollection.Addresses[0].Id;

            foreach (int _ in Enumerable.Range(0, 3))
            {
                if (addressCollection.HasMore is false or null)
                {
                    break;
                }

                addressCollection = await Client.Address.GetNextPage(addressCollection);
                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(firstId, addressCollection.Addresses[0].Id);

                firstId = addressCollection.Addresses[0].Id;
            }
        }

        /// <summary>
        ///     This test confirms that the GetNextPage method works with page limits.
        ///     This method is implemented on a per-service, but rather than testing in each service, we'll test it once here.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPageSizeLimit()
        {
            UseVCR("get_next_page_size_limit");

            AddressCollection addressCollection = await Client.Address.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            AddressCollection nextPageAddressCollection = await Client.Address.GetNextPage(addressCollection, Fixtures.PageSize);

            Assert.True(nextPageAddressCollection.Addresses.Count <= Fixtures.PageSize);
        }

        /// <summary>
        ///     This test confirms that the GetNextPage method will throw an EndOfPaginationError when it reaches the end of the list.
        ///     This method is implemented on a per-service, but rather than testing in each service, we'll test it once here.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Exception]
        public async Task TestGetNextPageReachEnd()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                // API call to get the page of addresses will return an empty list with HasMore = false
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/addresses"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new AddressCollection
                    {
                        Addresses = new List<Address>(),
                        HasMore = false,
                    })
                ),
            });

            AddressCollection addressCollection = await Client.Address.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            bool hitEnd = false;

            while (!hitEnd)
            {
                try
                {
                    addressCollection = await Client.Address.GetNextPage(addressCollection);
                }
                catch (EndOfPaginationError)
                {
                    hitEnd = true;
                }
            }

            Assert.True(hitEnd);
        }
    }
}
