using System;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.HttpTests
{
    public class ClientConfigurationTest
    {
        #region Tests

        [Fact]
        public void TestClientConfigurationDisposal()
        {
            ClientConfiguration configuration = new("not_a_real_api_key");
        }

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
                ApiBase = "https://www.example.com"
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
            ClientConfiguration configuration1 = new("fake_api_key")
            {
                ApiBase = "fake_api_key",
            };
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
        public void TestHashCode()
        {
            const string apiKey = "fake_api_key";
            const string apiBase = "https://www.example.com";
            TimeSpan timeout = TimeSpan.FromMilliseconds(5000);

            ClientConfiguration configuration1 = new(apiKey)
            {
                ApiBase = apiBase,
                Timeout = timeout,
            };

            // Assert that hashcode is calculated correctly
            int expectedHashCode = apiKey.GetHashCode() ^ apiBase.GetHashCode() ^ timeout.GetHashCode();
            Assert.Equal(expectedHashCode, configuration1.GetHashCode());
        }

        #endregion
    }
}
