﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class EventTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("event");

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            EventCollection eventCollection = await GetBasicEventCollection();

            List<Event> events = eventCollection.events;

            Assert.IsTrue(events.Count <= Fixture.PageSize);
            Assert.IsNotNull(eventCollection.has_more);
            foreach (Event item in events)
            {
                Assert.IsInstanceOfType(item, typeof(Event));
            }
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            EventCollection eventCollection = await GetBasicEventCollection();
            Event _event = eventCollection.events[0];

            Event retrievedEvent = await Event.Retrieve(_event.id);

            Assert.IsInstanceOfType(retrievedEvent, typeof(Event));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(_event.id, retrievedEvent.id);
        }

        private static async Task<EventCollection> GetBasicEventCollection() =>
            await Event.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
    }
}
