using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class BatchTest : UnitTest
    {
        public BatchTest() : base("batch", TestUtils.ApiKey.Test)
        {
        }

        [Fact]
        public async Task TestAddRemoveShipment()
        {
            UseVCR("add_remove_shipment");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.OneCallBuyShipment);

            Batch batch = await V2Client.Batches.Create();

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

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            BatchCollection batchCollection = await V2Client.Batches.All(new Dictionary<string, object>
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

        [Fact]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Batch batch = await CreateOneCallBuyBatch();

            // Uncomment the following line if you need to re-record the cassette
            // Thread.Sleep(5000); // Wait enough time for the batch to process buying the shipment
            await batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.num_shipments);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            Batch batch = await CreateBasicBatch();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.id.StartsWith("batch_"));
            Assert.IsNotNull(batch.shipments);
        }

        [Fact]
        public async Task TestCreateAndBuy()
        {
            UseVCR("create_and_buy");

            Batch batch = await V2Client.Batches.CreateAndBuy(new Dictionary<string, object>
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

        [Fact]
        public async Task TestCreateScanForm()
        {
            UseVCR("create_scan_form");


            Batch batch = await CreateOneCallBuyBatch();

            await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(15000); // Wait enough time to process
            }

            await batch.GenerateScanForm();

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [Fact]
        public async Task TestLabel()
        {
            UseVCR("label");

            Batch batch = await CreateOneCallBuyBatch();

            await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(15000); // Wait enough time to process
            }

            await batch.GenerateLabel("ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Batch batch = await CreateBasicBatch();

            Batch retrievedBatch = await V2Client.Batches.Retrieve(batch.id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.AreEqual(batch.id, retrievedBatch.id);
        }

        private async Task<Batch> CreateBasicBatch() =>
            await V2Client.Batches.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.BasicShipment
                    }
                }
            });

        private async Task<Batch> CreateOneCallBuyBatch() =>
            await V2Client.Batches.Create(new Dictionary<string, object>
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
