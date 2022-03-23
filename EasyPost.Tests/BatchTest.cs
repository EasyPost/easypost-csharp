using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class BatchTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "batch", true);
        }

        private static async Task<Batch> CreateBasicBatch()
        {
            return await Batch.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.BasicShipment
                    }
                }
            });
        }

        private static async Task<Batch> CreateOneCallBuyBatch()
        {
            return await Batch.Create(new Dictionary<string, object>
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
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Batch batch = await CreateBasicBatch();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.IsNotNull(batch.shipments);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            Batch batch = await CreateBasicBatch();

            Batch retrievedBatch = await Batch.Retrieve(batch.id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            Assert.AreEqual(batch.id, retrievedBatch.id);
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            BatchCollection batchCollection = await Batch.All(new Dictionary<string, object>
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

        [TestMethod]
        public async Task TestCreateAndBuy()
        {
            VCR.Replay("create_and_buy");

            Batch batch = await Batch.CreateAndBuy(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.OneCallBuyShipment
                    }
                }
            });

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.AreEqual(1, batch.num_shipments);
        }

        [TestMethod]
        public async Task TestBuy()
        {
            VCR.Replay("buy");

            Batch batch = await CreateOneCallBuyBatch();

            await batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.num_shipments);
        }

        [TestMethod]
        public async Task TestCreateScanForm()
        {
            VCR.Replay("create_scan_form");


            Batch batch = await CreateOneCallBuyBatch();
            await batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(2000); // Wait enough time for the batch to process buying the shipment

            await batch.GenerateScanForm();

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [TestMethod]
        public async Task TestAddRemoveShipment()
        {
            VCR.Replay("add_remove_shipment");

            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyShipment);

            Batch batch = await Batch.Create();

            await batch.AddShipments(new List<Shipment>
            {
                shipment
            });

            Assert.AreEqual(1, batch.num_shipments);

            await batch.RemoveShipments(new List<Shipment>
            {
                shipment
            });

            Assert.AreEqual(0, batch.num_shipments);
        }

        [TestMethod]
        public async Task TestLabel()
        {
            VCR.Replay("label");


            Batch batch = await CreateOneCallBuyBatch();
            await batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(2000); // Wait enough time for the batch to process buying the shipment

            await batch.GenerateLabel("ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }
    }
}
