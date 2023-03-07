using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class BatchTests : UnitTest
    {
        // NOTE: Due to an issue EasyVCR has with multiple exact matches of the same request in the same cassette, we have to split the add/remove overloads into separate unit tests.

        public BatchTests() : base("batch")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestAddShipments()
        {
            UseVCR("add_shipments");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            batch = await batch.AddShipments(new List<Shipment> { shipment });

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
            batch = await batch.AddShipments(new List<string> { shipment.Id });

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

            batch = await batch.AddShipments(shipmentsDictionary);

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

            batch = await batch.Buy();

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
        [Testing.Function]
        public async Task TestGenerateScanForm()
        {
            UseVCR("generate_scan_form");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } });

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
        [Testing.Function]
        public async Task TestRemoveShipments()
        {
            UseVCR("remove_shipments");

            Batch batch = await Client.Batch.Create();

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            batch = await batch.AddShipments(new List<Shipment> { shipment });
            Assert.Equal(1, batch.NumShipments);

            batch = await batch.RemoveShipments(new List<Shipment> { shipment });

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
            batch = await batch.AddShipments(new List<Shipment> { shipment });
            Assert.Equal(1, batch.NumShipments);

            batch = await batch.RemoveShipments(new List<string> { shipment.Id });

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
            batch = await batch.AddShipments(new List<Shipment> { shipment });
            Assert.Equal(1, batch.NumShipments);

            Dictionary<string, object> shipmentsDictionary = new() { { "shipments", new List<Dictionary<string, object>> { new() { { "id", shipment.Id } } } } };
            batch = await batch.RemoveShipments(shipmentsDictionary);

            Assert.Equal(0, batch.NumShipments);
        }

        #endregion

        #endregion
    }
}
