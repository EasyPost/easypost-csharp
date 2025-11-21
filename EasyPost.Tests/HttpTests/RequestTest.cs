using System.Collections.Generic;
using System.Net.Http;
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
        Request request = new("https://example.com", "not_a_real_endpoint", Method.Get, ApiVersion.V2, new Dictionary<string, object> { { "key", "value" } }, new Dictionary<string, string> { { "header_key", "header_value" } });
    }

    [Fact]
    public void TestQueryParameterList()
    {
        var parameters = new Dictionary<string, object>
        {
            { "key1", new List<string> { "value1", "value2" } },
            { "key2", "value3" }
        };

        Request request = new("https://example.com", "not_a_real_endpoint", Method.Get, ApiVersion.V2, parameters, new Dictionary<string, string> { { "header_key", "header_value" } });

        HttpRequestMessage httpRequestMessage = request.AsHttpRequestMessage();

        string queryString = httpRequestMessage.RequestUri?.Query;

        Assert.Contains("key1[]=value1", queryString); // Does not account for URL encoding
        Assert.Contains("key1[]=value2", queryString); // Does not account for URL encoding
        Assert.Contains("key2=value3", queryString); // Does not account for URL encoding
    }

    #endregion
}
