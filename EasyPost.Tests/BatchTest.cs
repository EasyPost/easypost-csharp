using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class BatchTest : UnitTest
    {
        public BatchTest() : base("batch")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Batch batch = await CreateBasicBatch();

            Assert.IsType<Batch>(batch);
            Assert.StartsWith("batch_", batch.Id);
            Assert.NotNull(batch.Shipments);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateAndBuy()
        {
            UseVCR("create_and_buy");

            Batch batch = await Client.Batch.CreateAndBuy(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixture.OneCallBuyShipment } } });

            Assert.IsType<Batch>(batch);
            Assert.StartsWith("batch_", batch.Id);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            BatchCollection batchCollection = await Client.Batch.All(new Dictionary<string, object> { { "page_size", Fixture.PageSize } });

            List<Batch> batches = batchCollection.Batches;

            Assert.True(batchCollection.HasMore);
            Assert.True(batches.Count <= Fixture.PageSize);
            foreach (Batch item in batches)
            {
                Assert.IsType<Batch>(item);
            }
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestAddRemoveShipments()
        {
            UseVCR("add_remove_shipments");

            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyShipment);

            Batch batch = await Client.Batch.Create();

            batch = await batch.AddShipments(new List<Shipment> { shipment });

            Assert.Equal(1, batch.NumShipments);

            batch = await batch.RemoveShipments(new List<Shipment> { shipment });

            Assert.Equal(0, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Batch batch = await CreateOneCallBuyBatch();

            batch = await batch.Buy();

            Assert.IsType<Batch>(batch);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestCreateScanForm()
        {
            UseVCR("create_scan_form");

            Batch batch = await CreateOneCallBuyBatch();

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await batch.GenerateScanForm();

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsType<Batch>(batch);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestLabel()
        {
            UseVCR("label");

            Batch batch = await CreateOneCallBuyBatch();

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            await batch.GenerateLabel("ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsType<Batch>(batch);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Batch batch = await CreateBasicBatch();

            Batch retrievedBatch = await Client.Batch.Retrieve(batch.Id);

            Assert.IsType<Batch>(retrievedBatch);
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.Equal(batch.Id, retrievedBatch.Id);
        }

        #endregion

        private async Task<Batch> CreateBasicBatch() =>
            await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixture.BasicShipment } } });

        private async Task<Batch> CreateOneCallBuyBatch() =>
            await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixture.OneCallBuyShipment } } });
    }
}
