using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class BatchTest : UnitTest
    {
        public BatchTest() : base("batch")
        {
        }

        [Fact]
        public async Task TestAddRemoveShipment()
        {
            UseVCR("add_remove_shipment", ApiVersion.Latest);

            Shipment shipment = await Client.Shipments.Create(new Shipments.Create(Fixture.OneCallBuyShipment));

            Batch batch = await Client.Batches.Create();

            batch = await batch.AddShipments(new List<Shipment>
            {
                shipment
            });

            Assert.AreEqual(1, batch.NumShipments);

            batch = await batch.RemoveShipments(new List<Shipment>
            {
                shipment
            });

            Assert.AreEqual(0, batch.NumShipments);
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            BatchCollection batchCollection = await Client.Batches.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<Batch> batches = batchCollection.Batches;

            Assert.IsTrue(batches.Count <= Fixture.PageSize);
            Assert.IsNotNull(batchCollection.HasMore);
            foreach (Batch item in batches)
            {
                Assert.IsInstanceOfType(item, typeof(Batch));
            }
        }

        [Fact]
        public async Task TestBuy()
        {
            UseVCR("buy", ApiVersion.Latest);

            Batch batch = await Client.CreateOneCallBuyBatch();

            batch = await batch.Buy();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.AreEqual(1, batch.NumShipments);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Batch batch = await Client.CreateBasicBatch();

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.Id.StartsWith("batch_"));
            Assert.IsNotNull(batch.Shipments);
        }

        [Fact]
        public async Task TestCreateAndBuy()
        {
            UseVCR("create_and_buy", ApiVersion.Latest);

            Batch batch = await Client.Batches.CreateAndBuy(new Batches.Create(new Dictionary<string, object>
            {
                {
                    "shipments", new List<Dictionary<string, object>>
                    {
                        Fixture.OneCallBuyShipment
                    }
                }
            }));

            Assert.IsInstanceOfType(batch, typeof(Batch));
            Assert.IsTrue(batch.Id.StartsWith("batch_"));
            Assert.AreEqual(1, batch.NumShipments);
        }

        [Fact]
        public async Task TestCreateScanForm()
        {
            UseVCR("create_scan_form", ApiVersion.Latest);


            Batch batch = await Client.CreateOneCallBuyBatch();

            batch = await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(15000); // Wait enough time to process
            }

            batch = await batch.GenerateScanForm(new Batches.CreateDocument
            {
                FileFormat = "PDF"
            });

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [Fact]
        public async Task TestLabel()
        {
            UseVCR("label", ApiVersion.Latest);

            Batch batch = await Client.CreateOneCallBuyBatch();

            batch = await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(15000); // Wait enough time to process
            }

            await batch.GenerateLabel(new Batches.CreateDocument
            {
                FileFormat = "ZPL"
            });

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsInstanceOfType(batch, typeof(Batch));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Batch batch = await Client.CreateBasicBatch();

            Batch retrievedBatch = await Client.Batches.Retrieve(batch.Id);

            Assert.IsInstanceOfType(retrievedBatch, typeof(Batch));
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.AreEqual(batch.Id, retrievedBatch.Id);
        }
    }
}
