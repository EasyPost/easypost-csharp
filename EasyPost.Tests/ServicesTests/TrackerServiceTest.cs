using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.ServicesTests
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

            TrackerCollection trackerCollection = await Client.Tracker.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

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

            Dictionary<string, object> filters = new Dictionary<string, object> {
                { "tracking_code", "0" },
                { "carrier", "test_carrier" },
            };

            TrackerCollection trackerCollection = await Client.Tracker.All(filters);

            Assert.Equal(filters["tracking_code"], trackerCollection.TrackingCode);
            Assert.Equal(filters["carrier"], trackerCollection.Carrier);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            TrackerCollection collection = await Client.Tracker.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                TrackerCollection nextPageCollection = await Client.Tracker.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Trackers[0].Id, nextPageCollection.Trackers[0].Id);
            }
            catch (EndOfPaginationError e) // There's no second page, that's not a failure
            {
                CustomAssertions.Pass();
            }
            catch // Any other exception is a failure
            {
                Assert.Fail("Failed to get next page");
            }
        }

        /// <summary>
        ///     This test confirms that the parameters used to filter the results of the All() method are used to filter the results of the GetNextPage() method.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Parameters]
        public async Task TestGetNextPageParameterHandOff()
        {
            UseVCR("get_next_page_parameter_hand_off");

            Dictionary<string, object> filters = new Dictionary<string, object> {
                { "tracking_code", "0" },
                { "carrier", "test_carrier" },
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

            BetaFeatures.Parameters.Trackers.All filtersForNextPage = trackerCollection.BuildNextPageParameters<BetaFeatures.Parameters.Trackers.All>(trackerCollection.Trackers);

            Assert.Equal(trackerCollection.TrackingCode, filtersForNextPage.TrackingCode);
            Assert.Equal(trackerCollection.Carrier, filtersForNextPage.Carrier);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await Client.Tracker.Create(Fixtures.Usps, "EZ1000000001");

            Tracker retrievedTracker = await Client.Tracker.Retrieve(tracker.Id);

            Assert.IsType<Tracker>(retrievedTracker);
            // Must compare IDs because other elements of objects may be different
            Assert.Equal(tracker.Id, retrievedTracker.Id);
        }

        #endregion

        #endregion
    }
}
