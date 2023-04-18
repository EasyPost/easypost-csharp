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
        public TrackerServiceTests() : base("tracker_service_with_parameters")
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

            BetaFeatures.Parameters.Trackers.Create parameters = new()
            {
                Carrier = Fixtures.Usps,
                TrackingCode = "EZ1000000001",
            };

            Tracker tracker = await Client.Tracker.Create(parameters);

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

            BetaFeatures.Parameters.Trackers.CreateList parameters = new();
            parameters.AddTracker("EZ1000000001");
            parameters.AddTracker("EZ1000000002");
            parameters.AddTracker("EZ1000000003");

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Tracker.CreateList(parameters));

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

        /// <summary>
        ///     This test confirms that the parameters used to filter the results of the All() method are passed through to the resulting collection object.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Parameters]
        public async Task TestAllParameterHandOff()
        {
            UseVCR("all_parameter_hand_off");

            BetaFeatures.Parameters.Trackers.All filters = new BetaFeatures.Parameters.Trackers.All
            {
                TrackingCode = "0",
                Carrier = "test_carrier",
            };

            TrackerCollection trackerCollection = await Client.Tracker.All(filters);

            // No trackers will match the filters, so the collection will be empty
            // Need to make a fake tracker temporarily to get the next page parameters
            Tracker fakeTracker = new Tracker
            {
                TrackingCode = "0",
                Carrier = "does_not_matter",
            };
            trackerCollection.Trackers.Add(fakeTracker);

            Assert.Equal(filters.TrackingCode, trackerCollection.TrackingCode);
            Assert.Equal(filters.Carrier, trackerCollection.Carrier);
        }

        #endregion

        #endregion
    }
}