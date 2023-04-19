using System;
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
            ClientConfiguration configuration1 = new(apiKey)
            {
                ApiBase = apiBase,
            };

            // Assert that the configuration is set correctly
            Assert.Equal(apiKey, configuration1.ApiKey);
            Assert.Equal(apiBase, configuration1.ApiBase);

            ClientConfiguration configuration2 = new("key2")
            {
                ApiBase = "https://www.example.com",
            };

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
            // Create two configurations, different keys, same base URL
            ClientConfiguration key1 = new("fake_api_key");
            ClientConfiguration key2 = new("key2");

            // Compare a configuration to null
            Assert.False(key1.Equals(null));
            Assert.False(key1 == null);

            // Compare a configuration to itself
            Assert.True(key1.Equals(key1));

            // Compare a configuration to a different type
            Assert.False(key1.Equals(0));

            // Compare two different configurations
            Assert.False(key1.Equals(key2));

            // Create two configurations, same key, different base URL
            ClientConfiguration base1 = new("fake_api_key")
            {
                ApiBase = "https://www.example.com",
            };
            ClientConfiguration base2 = new("fake_api_key")
            {
                ApiBase = "https://www.example2.com",
            };

            // Compare the two configurations
            Assert.False(base1.Equals(base2));

            // Compare different configurations to each other
            Assert.False(key1.Equals(base1));
            Assert.False(key1.Equals(base2));
            Assert.False(key2.Equals(base1));
            Assert.False(key2.Equals(base2));
        }

        #endregion
    }
}
