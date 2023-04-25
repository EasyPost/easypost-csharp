using System.Net.Http;
using EasyPost.Http;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.HttpTests
{
    public class ClientConfigurationTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestClientConfiguration()
        {
            const string apiKey = "fake_api_key";
            const string apiBase = "https://www.example.com";
            ClientConfiguration configuration1 = new(apiKey, apiBase);

            // Assert that the configuration is set correctly
            Assert.Equal(apiKey, configuration1.ApiKey);
            Assert.Equal(apiBase, configuration1.ApiBase);

            ClientConfiguration configuration2 = new("key2", "https://www.example.com");

            // Compare the two configurations
            Assert.False(configuration1.Equals(configuration2));

            // Compare the configuration to null
            Assert.False(configuration1.Equals(null));
            Assert.False(configuration1 == null);
        }

        [Fact]
        [Testing.Function]
        public void TestEquals()
        {
            ClientConfiguration configuration1 = new("fake_api_key", "https://www.example.com");
            ClientConfiguration configuration2 = new("key2", "https://www.example.com");

            // Compare the two configurations
            Assert.False(configuration1.Equals(configuration2));

            // Compare the configuration to null
            Assert.False(configuration1.Equals(null));
            Assert.False(configuration1 == null);
        }

        [Fact]
        [Testing.Function]
        public void TestHashCode()
        {
            const string apiKey = "fake_api_key";
            const string apiBase = "https://www.example.com";
            HttpClient httpClient = new();

            ClientConfiguration configuration1 = new(apiKey, apiBase, customHttpClient: httpClient);

            // Assert that hashcode is calculated correctly
            int expectedHashCode = apiKey.GetHashCode() ^ apiBase.GetHashCode() ^ httpClient.GetHashCode();
            Assert.Equal(expectedHashCode, configuration1.GetHashCode());
        }

        #endregion
    }
}
