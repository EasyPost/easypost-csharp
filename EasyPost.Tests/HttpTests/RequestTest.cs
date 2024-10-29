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
        Request request = new("https://google.com", "not_a_real_endpoint", Method.Get, ApiVersion.V2, new Dictionary<string, object> { { "key", "value" } }, new Dictionary<string, string> { { "header_key", "header_value" } });
        CustomAssertions.DoesNotThrow(() => request.Dispose());
    }

    [Fact]
    public void TestQueryParameterList()
    {
        const string key = "key";
        var value = new List<string>
        {
            "value1",
            "value2"
        };
        const string key2 = "key2";
        const string value2 = "value3";
        var parameters = new Dictionary<string, object>
        {
            { key, value },
            { key2, value2 }
        };

        Request request = new("https://google.com", "not_a_real_endpoint", Method.Get, ApiVersion.V2, parameters, new Dictionary<string, string> { { "header_key", "header_value" } });

        HttpRequestMessage httpRequestMessage = request.AsHttpRequestMessage();

        string queryString = httpRequestMessage.RequestUri?.Query;

        string keyWithArray = $"{key}[]";

        foreach (string item in value)
        {
            Assert.Contains($"{keyWithArray}={item}", queryString); // Does not account for URL encoding
        }
        Assert.Contains($"{key2}={value2}", queryString); // Does not account for URL encoding
    }

    #endregion
}
