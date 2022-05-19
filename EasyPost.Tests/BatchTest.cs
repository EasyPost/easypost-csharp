using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class BatchTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("batch");

        [TestMethod]
        public async Task TestAddRemoveShipment()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("add_remove_shipment");

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
        public async Task TestAll()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("all");

            BatchCollection batchCollection = await client.Batches.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Batch> batches = batchCollection.batches;

            Assert.IsTrue(batches.Count <= Fixture.PageSize);
            Assert.IsNotNull(batchCollection.has_more);
            foreach (Batch item in batches)
            {
                Assert.IsInstanceOfType(item, typeof(Batch));
            }
        }

        [TestMethod]
        public async Task TestBuy()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("buy");

            Batch batch = await CreateOneCallBuyBatch(client);

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(5000); // Wait enough time for the batch to process buying the shipment
            await batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.num_shipments);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create");

            Batch batch = await CreateBasicBatch(client);

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.IsNotNull(batch.shipments);
        }

        [TestMethod]
        public async Task TestCreateAndBuy()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create_and_buy");

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
        public async Task TestCreateScanForm()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create_scan_form");


            Batch batch = await CreateOneCallBuyBatch(client);

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(5000); // Wait enough time for the batch to process buying the shipment
            await batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(5000); // Wait enough time for the batch to process

            await batch.GenerateScanForm();

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [TestMethod]
        public async Task TestLabel()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("label");


            Batch batch = await CreateOneCallBuyBatch(client);

            // Uncomment the following line if you need to re-record the cassette
            // hread.Sleep(5000); // Wait enough time for the batch to process buying the shipment
            await batch.Buy();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(5000); // Wait enough time for the batch to process

            await batch.GenerateLabel("ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("retrieve");


            Batch batch = await CreateBasicBatch(client);

            Batch retrievedBatch = await client.Batches.Retrieve(batch.id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.AreEqual(batch.id, retrievedBatch.id);
        }

        private static async Task<Batch> CreateBasicBatch(V2Client client) =>
            await client.Batches.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.BasicShipment
                    }
                }
            });

        private static async Task<Batch> CreateOneCallBuyBatch(V2Client client) =>
            await client.Batches.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.OneCallBuyShipment
                    }
                }
            });
    }
}
