using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class BatchServiceTests : UnitTest
    {
        public BatchServiceTests() : base("batch_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } });

            Assert.IsType<Batch>(batch);
            Assert.StartsWith("batch_", batch.Id);
            Assert.NotNull(batch.Shipments);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateAndBuy()
        {
            UseVCR("create_and_buy");

            Batch batch = await Client.Batch.CreateAndBuy(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } });

            Assert.IsType<Batch>(batch);
            Assert.StartsWith("batch_", batch.Id);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            BatchCollection batchCollection = await Client.Batch.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Batch> batches = batchCollection.Batches;

            Assert.True(batches.Count <= Fixtures.PageSize);
            foreach (Batch item in batches)
            {
                Assert.IsType<Batch>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } });

            Batch retrievedBatch = await Client.Batch.Retrieve(batch.Id);

            Assert.IsType<Batch>(retrievedBatch);
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.Equal(batch.Id, retrievedBatch.Id);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestAddShipments()
        {
            UseVCR("add_shipments");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            batch = await Client.Batch.AddShipments(batch.Id, new List<Shipment> { shipment });

            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestAddShipmentsById()
        {
            UseVCR("add_shipments_by_id");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            batch = await Client.Batch.AddShipments(batch.Id, new List<string> { shipment.Id });

            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestAddShipmentsByDictionary()
        {
            UseVCR("add_shipments_by_dictionary");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> shipmentsDictionary = new() { { "shipments", new List<Dictionary<string, object>> { new() { { "id", shipment.Id } } } } };

            batch = await Client.Batch.AddShipments(batch.Id, shipmentsDictionary);

            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } });

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await Client.Batch.Buy(batch.Id);

            Assert.IsType<Batch>(batch);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } });

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await Client.Batch.Buy(batch.Id);

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            await Client.Batch.GenerateLabel(batch.Id, "ZPL");

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsType<Batch>(batch);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateScanForm()
        {
            UseVCR("generate_scan_form");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } });

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await Client.Batch.Buy(batch.Id);

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await Client.Batch.GenerateScanForm(batch.Id, "pdf");

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsType<Batch>(batch);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRemoveShipments()
        {
            UseVCR("remove_shipments");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            batch = await Client.Batch.AddShipments(batch.Id, new List<Shipment> { shipment });
            Assert.Equal(1, batch.NumShipments);

            batch = await Client.Batch.RemoveShipments(batch.Id, new List<Shipment> { shipment });

            Assert.Equal(0, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestRemoveShipmentsById()
        {
            UseVCR("remove_shipments_by_id");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            batch = await Client.Batch.AddShipments(batch.Id, new List<Shipment> { shipment });
            Assert.Equal(1, batch.NumShipments);

            batch = await Client.Batch.RemoveShipments(batch.Id, new List<string> { shipment.Id });

            Assert.Equal(0, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestRemoveShipmentsByDictionary()
        {
            UseVCR("remove_shipments_by_dictionary");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            batch = await Client.Batch.AddShipments(batch.Id, new List<Shipment> { shipment });
            Assert.Equal(1, batch.NumShipments);

            Dictionary<string, object> shipmentsDictionary = new() { { "shipments", new List<Dictionary<string, object>> { new() { { "id", shipment.Id } } } } };
            batch = await Client.Batch.RemoveShipments(batch.Id, shipmentsDictionary);

            Assert.Equal(0, batch.NumShipments);
        }

        #endregion

        #endregion
    }
}
