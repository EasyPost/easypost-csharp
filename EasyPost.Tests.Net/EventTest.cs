using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class EventTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "event", true);
        }

        private static EventCollection GetBasicEventCollection()
        {
            return Event.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
        }

        [TestMethod]
        public void TestAll()
        {
            VCR.Replay("all");

            EventCollection eventCollection = GetBasicEventCollection();

            List<Event> events = eventCollection.events;

            Assert.IsTrue(events.Count <= Fixture.PageSize);
            Assert.IsNotNull(eventCollection.has_more);
            foreach (var item in events)
            {
                Assert.IsInstanceOfType(item, typeof(Event));
            }
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");


            EventCollection eventCollection = GetBasicEventCollection();
            Event _event = eventCollection.events[0];

            Event retrievedEvent = Event.Retrieve(_event.id);

            Assert.IsInstanceOfType(retrievedEvent, typeof(Event));
            Assert.AreEqual(_event.id, retrievedEvent.id);
        }

        [TestMethod]
        public void TestRetrieveBadInput()
        {
            VCR.Replay("retrieve_bad_input");

            Assert.ThrowsException<ApiException>(() => Event.Retrieve("bad input"));
        }

        [TestMethod]
        public void TestRetrieveNoInput()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Event.Retrieve(""));
        }
    }
}
