using System.Collections.Generic;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
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
            Dictionary<SmartrateAccuracy, int> pairs = new()
            {
                { SmartrateAccuracy.Percentile50, 1 },
                { SmartrateAccuracy.Percentile75, 2 },
                { SmartrateAccuracy.Percentile85, 3 },
                { SmartrateAccuracy.Percentile90, 4 },
                { SmartrateAccuracy.Percentile95, 5 },
                { SmartrateAccuracy.Percentile97, 6 },
                { SmartrateAccuracy.Percentile99, 7 }
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

            foreach (KeyValuePair<SmartrateAccuracy, int> pair in pairs)
            {
                SmartrateAccuracy accuracy = pair.Key;
                int expected = pair.Value;
                int? actual = timeInTransit.GetBySmartrateAccuracy(accuracy);
                Assert.Equal(expected, actual);
            }
        }
    }
}
