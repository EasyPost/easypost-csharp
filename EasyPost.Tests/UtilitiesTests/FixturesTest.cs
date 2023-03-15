using System.Collections.Generic;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    public class FixturesTest
    {
        /// <summary>
        ///     This test confirms that the patterns used in unit tests to convert fixture data dictionaries into <see cref="BetaFeatures.Parameters"/> objects are working as expected.
        ///
        ///     If the patterns were not working properly, the rest of the unit tests that utilize this pattern of generating <see cref="BetaFeatures.Parameters"/> could be invisibly using invalid data.
        /// </summary>
        [Fact]
        [Testing.Function]
        public void TestGenerateParametersObjectFromFixtureDictionary()
        {
            Dictionary<string, object> fixture = Fixtures.BasicCarrierAccount;

            BetaFeatures.Parameters.CarrierAccounts.Create parameters = new()
            {
                Credentials = fixture.GetOrNull<Dictionary<string, object?>>("credentials"),
                TestCredentials = fixture.GetOrNull<Dictionary<string, object?>>("test_credentials"),
                Description = fixture.GetOrNull<string>("description"),
                Reference = fixture.GetOrNull<string>("reference"),
                Type = fixture.GetOrNull<string>("type"),
            };

            // Test a string value
            Assert.Equal(fixture["type"], parameters.Type);

            // Test a dictionary value
            Dictionary<string, object?> credentials = fixture.GetOrNull<Dictionary<string, object?>>("credentials");
            // We want to make sure we actually extracted a dictionary from the fixture data. Comparing two null values would be pointless.
            Assert.NotNull(credentials);
            Assert.Equal(credentials, parameters.Credentials);
        }
    }
}
