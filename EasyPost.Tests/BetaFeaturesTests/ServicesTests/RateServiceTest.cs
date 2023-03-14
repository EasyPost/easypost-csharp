using System.Collections.Generic;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class RateServiceTests : UnitTest
    {
        public RateServiceTests() : base("rate_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        #endregion

        [Fact]
        [Testing.Function]
        public void TestGetLowestRate()
        {
            UseVCR("get_lowest_rate");

            List<Rate> rates = new()
            {
                new Rate { Price = "100.00" },
                new Rate { Price = "1.00" },
            };

            Rate lowestRate = Client.Rate.GetLowestRate(rates);
            Assert.Equal("1.00", lowestRate.Price);
        }

        #endregion
    }
}
