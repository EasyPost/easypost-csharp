using System.Collections.Generic;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class TimeInTransitTests : UnitTest
    {
        public TimeInTransitTests() : base("time_in_transit")
        {
        }

        [Fact]
        [Testing.Function]
        public void TestGetBySmartRateAccuracy()
        {
            Dictionary<SmartRateAccuracy, int> pairs = new()
            {
                { SmartRateAccuracy.Percentile50, 1 },
                { SmartRateAccuracy.Percentile75, 2 },
                { SmartRateAccuracy.Percentile85, 3 },
                { SmartRateAccuracy.Percentile90, 4 },
                { SmartRateAccuracy.Percentile95, 5 },
                { SmartRateAccuracy.Percentile97, 6 },
                { SmartRateAccuracy.Percentile99, 7 }
            };

            TimeInTransit timeInTransit = new()
            {
                Percentile50 = 1,
                Percentile75 = 2,
                Percentile85 = 3,
                Percentile90 = 4,
                Percentile95 = 5,
                Percentile97 = 6,
                Percentile99 = 7
            };

            foreach (KeyValuePair<SmartRateAccuracy, int> pair in pairs)
            {
                SmartRateAccuracy accuracy = pair.Key;
                int expected = pair.Value;
                int? actual = timeInTransit.GetBySmartRateAccuracy(accuracy);
                Assert.Equal(expected, actual);
            }
        }
    }
}
