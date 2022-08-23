using System.Collections.Generic;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class EnumTest
    {
        [Fact]
        public void TestEnumeration()
        {
            // going to use SmartrateAccuracy enums for this test,
            // Enumeration is an abstract class, we need to use some implementation of it to test the functionality

            // two of the same enum
            SmartrateAccuracy accuracy1 = SmartrateAccuracy.Percentile50;
            SmartrateAccuracy accuracy2 = SmartrateAccuracy.Percentile50;

            Assert.True(accuracy1.Equals(accuracy2));
            // compareTo checks if accuracy1 comes before accuracy2
            // since they are the same, it should return 0
            Assert.True(accuracy1.CompareTo(accuracy2) == 0);

            // two different enums
            SmartrateAccuracy accuracy3 = SmartrateAccuracy.Percentile50;
            SmartrateAccuracy accuracy4 = SmartrateAccuracy.Percentile95;

            Assert.False(accuracy3.Equals(accuracy4));
            // compareTo checks if accuracy3 comes before accuracy4
            // since accuracy3 is before accuracy4, it should return -1
            Assert.True(accuracy3.CompareTo(accuracy4) == -1);

            // get a list of all the enum values
            List<string> enumValues = new List<string>
            {
                "percentile_50",
                "percentile_75",
                "percentile_85",
                "percentile_90",
                "percentile_95",
                "percentile_97",
                "percentile_99"
            };
            IEnumerable<SmartrateAccuracy> accuracies = SmartrateAccuracy.All();
            foreach (var accuracy in accuracies)
            {
                Assert.IsType<SmartrateAccuracy>(accuracy);
                Assert.Contains(enumValues, x => x == (string)accuracy.Value);
            }
        }
    }
}
