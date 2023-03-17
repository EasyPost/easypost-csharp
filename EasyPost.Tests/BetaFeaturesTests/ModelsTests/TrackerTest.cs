using System.Threading.Tasks;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
    public class TrackerTests : UnitTest
    {
        public TrackerTests() : base("tracker_with_parameters")
        {
        }

        [Fact]
        [Testing.Properties]
#pragma warning disable CS1998
        public async Task TestTrackerCarrierDetails()
#pragma warning restore CS1998
        {
            // Details can be (and most likely will be) missing or incomplete, so we can't reliably test them.
        }
    }
}
