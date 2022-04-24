using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class BatchTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("batch");
        }

        private static async Task<Batch> CreateBasicBatch(Client client)
        {
            return await client.Batches.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.BasicShipment
                    }
                }
            });
        }

        private static async Task<Batch> CreateOneCallBuyBatch(Client client)
        {
            return await client.Batches.Create(new Dictionary<string, object>
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
            Client client = _vcr.SetUpTest("create");

            Batch batch = await CreateBasicBatch(client);

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.IsNotNull(batch.shipments);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            Client client = _vcr.SetUpTest("retrieve");


            Batch batch = await CreateBasicBatch(client);

            Batch retrievedBatch = await client.Batches.Retrieve(batch.id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.AreEqual(batch.id, retrievedBatch.id);
        }

        [TestMethod]
        public async Task TestAll()
        {
            Client client = _vcr.SetUpTest("all");

            BatchCollection batchCollection = await client.Batches.All(new Dictionary<string, object>
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
            Client client = _vcr.SetUpTest("create_and_buy");

            Batch batch = await client.Batches.CreateAndBuy(new Dictionary<string, object>
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
            Client client = _vcr.SetUpTest("buy");

            Batch batch = await CreateOneCallBuyBatch(client);

            await batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.num_shipments);
        }

        [TestMethod]
        public async Task TestCreateScanForm()
        {
            Client client = _vcr.SetUpTest("create_scan_form");


            Batch batch = await CreateOneCallBuyBatch(client);
            await batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(2000); // Wait enough time for the batch to process

            await batch.GenerateScanForm();

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [TestMethod]
        public async Task TestAddRemoveShipment()
        {
            Client client = _vcr.SetUpTest("add_remove_shipment");

            Shipment shipment = await client.Shipments.Create(Fixture.OneCallBuyShipment);

            Batch batch = await client.Batches.Create();

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
            Client client = _vcr.SetUpTest("label");


            Batch batch = await CreateOneCallBuyBatch(client);

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(2000); // Wait enough time for the batch to process buying the shipment
            await batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(2000); // Wait enough time for the batch to process

            await batch.GenerateLabel("ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }
    }
}
