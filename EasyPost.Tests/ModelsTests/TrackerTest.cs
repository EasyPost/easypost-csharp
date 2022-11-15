using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class TrackerTests : UnitTest
    {
        public TrackerTests() : base("tracker")
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

        [Fact]
        [Testing.Properties]
        public async Task TestTrackerTrackingDetails()
        {
            UseVCR("tracker_tracking_details");

            Tracker tracker = await CreateBasicTracker();

            Assert.NotNull(tracker.TrackingDetails);
            foreach (TrackingDetail trackingDetail in tracker.TrackingDetails)
            {
                Assert.NotNull(trackingDetail.Message);
                Assert.NotNull(trackingDetail.Status);
                Assert.NotNull(trackingDetail.Datetime);
                Assert.NotNull(trackingDetail.TrackingLocation);
                // TrackingLocation details will be empty, so we can't test their presence.
            }
        }

        private async Task<Tracker> CreateBasicTracker() => await Client.Tracker.Create(Fixtures.Usps, "EZ1000000001");
    }
}
