using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class TrackerTests() : UnitTest("tracker")
    {
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

            Tracker tracker = await Client.Tracker.Create(Fixtures.Usps, "EZ1000000001");

            Assert.NotNull(tracker.TrackingDetails);
            foreach (TrackingDetail trackingDetail in tracker.TrackingDetails)
            {
                Assert.NotNull(trackingDetail.Message);
                Assert.NotNull(trackingDetail.Status);
                Assert.NotNull(trackingDetail.StatusDetail);
                Assert.NotNull(trackingDetail.Datetime);
                Assert.NotNull(trackingDetail.TrackingLocation);
                // TrackingLocation details will be empty, so we can't test their presence.
            }
        }

        [Fact]
        [Testing.Properties]
        public void TestTrackerDeliveryEvidence()
        {
            const string json = "{\"id\": \"trk_123\", \"object\": \"Tracker\", \"delivery_evidence\": [{\"type\": \"signature\", \"image\": \"https://example.com/signature.png\"}]}";

            Tracker tracker = JsonSerialization.ConvertJsonToObject<Tracker>(json);

            Assert.NotNull(tracker.DeliveryEvidence);
            DeliveryEvidence deliveryEvidence = Assert.Single(tracker.DeliveryEvidence);
            Assert.Equal("signature", deliveryEvidence.Type);
            Assert.Equal("https://example.com/signature.png", deliveryEvidence.Image);
        }
    }
}
