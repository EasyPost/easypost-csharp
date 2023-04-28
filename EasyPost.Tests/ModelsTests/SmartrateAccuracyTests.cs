using System.Collections.Generic;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class SmartRateAccuracyTests : UnitTest
    {
        public SmartRateAccuracyTests() : base("smart_rate_accuracy")
        {
        }

        [Fact]
        [Testing.Function]
        public void TestAll()
        {
            IEnumerable<SmartRateAccuracy> enums = SmartRateAccuracy.All();
            foreach (SmartRateAccuracy? @enum in enums)
            {
                Assert.IsType<SmartRateAccuracy>(@enum);
                Assert.IsType<string>(@enum.Value);
            }
        }
    }
}
