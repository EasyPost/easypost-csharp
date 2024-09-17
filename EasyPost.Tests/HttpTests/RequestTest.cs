using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Http;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.HttpTests;

public class RequestTest
{
    #region Tests

    [Fact]
    public void TestRequestDisposal()
    {
        Request request = new("https://google.com", "not_a_real_endpoint", Method.Get, ApiVersion.V2, new Dictionary<string, object> { { "key", "value" } }, new Dictionary<string, string> { { "header_key", "header_value" } });
        CustomAssertions.DoesNotThrow(() => request.Dispose());
    }

    #endregion
}
