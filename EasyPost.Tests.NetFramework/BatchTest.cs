using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class BatchTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        private static Batch CreateBasicBatch()
        {
            return Batch.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.BasicShipment
                    }
                }
            });
        }

        private static Batch CreateOneCallBuyBatch()
        {
            return Batch.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.OneCallBuyShipment
                    }
                }
            });
        }

        [TestMethod]
        public void TestCreate()
        {
            Batch batch = CreateBasicBatch();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.IsNotNull(batch.shipments);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            Batch batch = CreateBasicBatch();

            Batch retrievedBatch = Batch.Retrieve(batch.id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            Assert.AreEqual(batch.id, retrievedBatch.id);
        }

        [TestMethod]
        public void TestAll()
        {
            BatchCollection batchCollection = Batch.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Batch> batches = batchCollection.batches;

            Assert.IsTrue(batches.Count <= Fixture.PageSize);
            Assert.IsNotNull(batchCollection.has_more);
            foreach (var item in batches)
            {
                Assert.IsInstanceOfType(item, typeof(Batch));
            }
        }

        // TODO: C# doesn't have a CreateAndBuy method for Batch
        [Ignore]
        [TestMethod]
        public void TestCreateAndBuy()
        {
        }

        // hard to test because the sleep time is hit-or-miss
        [Ignore]
        [TestMethod]
        public void TestBuy()
        {
            var data = Fixture.OneCallBuyShipment;

            Batch batch = CreateOneCallBuyBatch();

            batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.num_shipments);
        }

        // hard to test because the sleep time is hit-or-miss
        [Ignore]
        [TestMethod]
        public void TestCreateScanForm()
        {
            Batch batch = CreateOneCallBuyBatch();
            batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            Thread.Sleep(10000); // Wait enough time for the batch to process buying the shipment

            batch.GenerateScanForm();

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [TestMethod]
        public void TestAddRemoveShipment()
        {
            Shipment shipment = Shipment.Create(Fixture.OneCallBuyShipment);

            Batch batch = Batch.Create();

            batch.AddShipments(new List<Shipment>
            {
                shipment
            });

            Assert.AreEqual(1, batch.num_shipments);

            batch.RemoveShipments(new List<Shipment>
            {
                shipment
            });

            Assert.AreEqual(0, batch.num_shipments);
        }

        // hard to test because the sleep time is hit-or-miss
        [Ignore]
        [TestMethod]
        public void TestLabel()
        {
            Batch batch = CreateOneCallBuyBatch();
            batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            Thread.Sleep(10000); // Wait enough time for the batch to process buying the shipment

            batch.GenerateLabel("ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }
    }
}
