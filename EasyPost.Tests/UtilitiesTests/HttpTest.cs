using System.Net;
using System.Net.Http;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    public class HttpTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestStatusCodeChecksHttpStatusCode()
        {
            const HttpStatusCode statusCode = HttpStatusCode.OK;

            Assert.True(Utilities.Internal.Extensions.Http.StatusCodeBetween(statusCode, 200, 300));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs1xx(statusCode));
            Assert.True(Utilities.Internal.Extensions.Http.StatusCodeIs2xx(statusCode));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs3xx(statusCode));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs4xx(statusCode));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs5xx(statusCode));

            Assert.True(statusCode.IsBetween(200, 300));
            Assert.False(statusCode.Is1xx());
            Assert.True(statusCode.Is2xx());
            Assert.False(statusCode.Is3xx());
            Assert.False(statusCode.Is4xx());
            Assert.False(statusCode.Is5xx());
        }

        [Fact]
        [Testing.Function]
        public void TestStatusCodeChecksInts()
        {
            const int statusCode = 200;

            Assert.True(Utilities.Internal.Extensions.Http.StatusCodeBetween(statusCode, 200, 300));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs1xx(statusCode));
            Assert.True(Utilities.Internal.Extensions.Http.StatusCodeIs2xx(statusCode));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs3xx(statusCode));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs4xx(statusCode));
            Assert.False(Utilities.Internal.Extensions.Http.StatusCodeIs5xx(statusCode));
        }

        [Fact]
        [Testing.Function]
        public void TestStatusCodeChecksHttpResponseMessage()
        {
            HttpResponseMessage response = new() { StatusCode = HttpStatusCode.OK };

            Assert.True(Utilities.Internal.Extensions.Http.StatusCodeBetween(response, 200, 300));
            Assert.True(response.HasStatusCodeBetween(200, 300));
        }

        #endregion
    }
}
