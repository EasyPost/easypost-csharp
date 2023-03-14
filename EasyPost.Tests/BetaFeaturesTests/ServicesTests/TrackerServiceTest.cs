using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class TrackerServiceTests : UnitTest
    {
        public TrackerServiceTests() : base("tracker_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Tracker tracker = await Client.Tracker.Create(Fixtures.Usps, "EZ1000000001");

            Assert.IsType<Tracker>(tracker);
            Assert.StartsWith("trk_", tracker.Id);
            Assert.Equal("pre_transit", tracker.Status);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateList()
        {
            UseVCR("create_list");

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Tracker.CreateList(new Dictionary<string, object>
            {
                { "0", new Dictionary<string, object> { { "tracking_code", "EZ1000000001" } } },
                { "1", new Dictionary<string, object> { { "tracking_code", "EZ1000000002" } } },
                { "2", new Dictionary<string, object> { { "tracking_code", "EZ1000000003" } } }
            }));

            Assert.Null(possibleException);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Trackers.All parameters = Fixtures.Parameters.Trackers.All(data);

            TrackerCollection trackerCollection = await Client.Tracker.All(parameters);

            List<Tracker> trackers = trackerCollection.Trackers;

            Assert.True(trackers.Count <= Fixtures.PageSize);
            foreach (Tracker tracker in trackers)
            {
                Assert.IsType<Tracker>(tracker);
            }
        }

        #endregion

        #endregion
    }
}
