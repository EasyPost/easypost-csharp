using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
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

        private static async Task<Batch> CreateBasicBatch(V2Client v2Client)
        {
            return await v2Client.Batches.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.BasicShipment
                    }
                }
            });
        }

        private static async Task<Batch> CreateOneCallBuyBatch(V2Client v2Client)
        {
            return await v2Client.Batches.Create(new Dictionary<string, object>
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
            V2Client v2Client = _vcr.SetUpTest("create");

            Batch batch = await CreateBasicBatch(v2Client);

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.IsNotNull(batch.shipments);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");


            Batch batch = await CreateBasicBatch(v2Client);

            Batch retrievedBatch = await v2Client.Batches.Retrieve(batch.id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.AreEqual(batch.id, retrievedBatch.id);
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client v2Client = _vcr.SetUpTest("all");

            BatchCollection batchCollection = await v2Client.Batches.All(new Dictionary<string, object>
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
            V2Client v2Client = _vcr.SetUpTest("create_and_buy");

            Batch batch = await v2Client.Batches.CreateAndBuy(new Dictionary<string, object>
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
            V2Client v2Client = _vcr.SetUpTest("buy");

            Batch batch = await CreateOneCallBuyBatch(v2Client);

            await batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.num_shipments);
        }

        [TestMethod]
        public async Task TestCreateScanForm()
        {
            V2Client v2Client = _vcr.SetUpTest("create_scan_form");


            Batch batch = await CreateOneCallBuyBatch(v2Client);
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
            V2Client v2Client = _vcr.SetUpTest("add_remove_shipment");

            Shipment shipment = await v2Client.Shipments.Create(Fixture.OneCallBuyShipment);

            Batch batch = await v2Client.Batches.Create();

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
            V2Client v2Client = _vcr.SetUpTest("label");


            Batch batch = await CreateOneCallBuyBatch(v2Client);

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
