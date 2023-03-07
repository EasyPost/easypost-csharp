using System.Collections.Generic;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.baseTests
{
    public class EasyPostObjectTests
    {
        [Fact]
        [Testing.Properties]
        public void TestPrefix()
        {
            Address address = new()
            {
                Id = "adr_123",
            };
            Assert.Equal("adr", address.Prefix);
        }

        [Fact]
        [Testing.EdgeCase]
        public void TestPrefixWhenIdIsNull()
        {
            Address address = new();
            address.Id = null;

            Assert.Null(address.Prefix);
        }

        [Fact]
        [Testing.Function]
        public void TestHashCode()
        {
            ApiKey apiKey = new() { Id = "adr_123" };
            ApiKey sameProperties = new() { Id = "adr_123" };
            ApiKey differentProperties = new() { Id = "adr_456" };

            // hashcode should be the same number each time
            Assert.Equal(apiKey.GetHashCode(), apiKey.GetHashCode());

            // hashcode should be the same for two objects with the same properties (including Client)
            Assert.Equal(apiKey.GetHashCode(), sameProperties.GetHashCode());

            // hashcode should be different for two objects with different properties
            Assert.NotEqual(apiKey.GetHashCode(), differentProperties.GetHashCode());

            // hashcode should be different for two objects with the same properties but different Clients
            sameProperties.Client = new Client("key");
            Assert.NotEqual(apiKey.GetHashCode(), sameProperties.GetHashCode());
        }

        [Fact]
        [Testing.Function]
        public void TestEquals()
        {
            ApiKey apiKey = new() { Id = "adr_123" };
            ApiKey sameProperties = new() { Id = "adr_123" };
            ApiKey differentProperties = new() { Id = "adr_456" };

            // Equality under-the-hood is based on hashcode, which is based on properties/client of an object
            // so if two objects have the same properties and client, they should be equal

            // two objects with the same properties should be equal
            Assert.Equal(apiKey, sameProperties);
            Assert.True(apiKey.Equals(sameProperties));
            Assert.True(apiKey == sameProperties);
            Assert.False(apiKey != sameProperties);

            // two objects with different properties should not be equal
            Assert.NotEqual(apiKey, differentProperties);
            Assert.False(apiKey.Equals(differentProperties));
            Assert.False(apiKey == differentProperties);
            Assert.True(apiKey != differentProperties);

            // two objects with the same properties but different clients should not be equal
            sameProperties.Client = new Client("key");
            Assert.NotEqual(apiKey, sameProperties);
            Assert.False(apiKey.Equals(sameProperties));
            Assert.False(apiKey == sameProperties);
            Assert.True(apiKey != sameProperties);

            // two objects of different types should not be equal
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(apiKey.Equals(new List<string>()));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(apiKey.Equals(new Address()));

            // comparing an object to null should not be equal
            Assert.False(apiKey.Equals(null));
            Assert.False(apiKey == null);
            Assert.False(null == apiKey);
        }
    }
}
