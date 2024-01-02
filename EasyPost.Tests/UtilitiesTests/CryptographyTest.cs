using System.Diagnostics.CodeAnalysis;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities;
using Xunit;

// ReSharper disable InconsistentNaming

namespace EasyPost.Tests.UtilitiesTests
{
    public class CryptographyTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestByteArrayAsString()
        {
            byte[] testBytes = "test"u8.ToArray();
            const string expected = "test";

            Assert.Equal(expected, testBytes.AsString());
        }

        [Fact]
        [Testing.Function]
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        public void TestCalculateHMACSHA256HexDigest()
        {
            // Test is handled in WebhookTest
        }

        [Fact]
        [Testing.Function]
        public void TestSignaturesMatch()
        {
            const string signature1S = "test";
            const string signature2S = "not test";

            Assert.True(Cryptography.SignaturesMatch(signature1S, signature1S)); // same signatures
            Assert.False(Cryptography.SignaturesMatch(signature1S, signature2S)); // different signatures
            Assert.False(Cryptography.SignaturesMatch(signature1S, null)); // one signature is null
            Assert.False(Cryptography.SignaturesMatch(signature1S, "longer_string")); // signatures are different lengths

            byte[] signature1B = "test"u8.ToArray();
            byte[] signature2B = "tesu"u8.ToArray();

            Assert.True(Cryptography.SignaturesMatch(signature1B, signature1B)); // same signatures
            Assert.False(Cryptography.SignaturesMatch(signature1B, signature2B)); // different signatures
            Assert.False(Cryptography.SignaturesMatch(signature1B, null)); // one signature is null
            Assert.False(Cryptography.SignaturesMatch(signature1B, new[] { (byte)0x74 })); // signatures are different lengths
        }

        [Fact]
        [Testing.Function]
        public void TestStringAsHexString()
        {
            const string testString = "test";
            const string expected = "74657374";

            Assert.Equal(expected, testString.AsHexString());
        }

        #endregion
    }
}
