using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class BatchServiceTests : UnitTest
    {
        public BatchServiceTests() : base("batch_service_with_parameters")
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

            Dictionary<string, object> data = new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } };

            BetaFeatures.Parameters.Batches.Create parameters = Fixtures.Parameters.Batches.Create(data);

            Batch batch = await Client.Batch.Create(parameters);

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

            Dictionary<string, object> data = new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } };

            BetaFeatures.Parameters.Batches.Create parameters = Fixtures.Parameters.Batches.Create(data);

            Batch batch = await Client.Batch.CreateAndBuy(parameters);

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

            Dictionary<string, object> data = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Batches.All parameters = Fixtures.Parameters.Batches.All(data);

            BatchCollection batchCollection = await Client.Batch.All(parameters);

            List<Batch> batches = batchCollection.Batches;

            Assert.True(batches.Count <= Fixtures.PageSize);
            foreach (Batch item in batches)
            {
                Assert.IsType<Batch>(item);
            }
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateWithShipments()
        {
            UseVCR("create_with_shipments");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            BetaFeatures.Parameters.Batches.Create batchParameters = new BetaFeatures.Parameters.Batches.Create
            {
                Shipments = new List<IShipmentParameter> { shipment },
            };

            Batch batch = await Client.Batch.Create(batchParameters);

            Assert.IsType<Batch>(batch);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateWithShipmentParameters()
        {
            UseVCR("create_with_shipment_parameters");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            BetaFeatures.Parameters.Batches.Create batchParameters = new BetaFeatures.Parameters.Batches.Create
            {
                Shipments = new List<IShipmentParameter> { shipmentParameters },
            };

            Batch batch = await Client.Batch.Create(batchParameters);

            Assert.IsType<Batch>(batch);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestAddShipments()
        {
            UseVCR("add_shipments");

            Batch batch = await Client.Batch.Create();

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            BetaFeatures.Parameters.Batches.AddShipments addShipmentsParameters = new BetaFeatures.Parameters.Batches.AddShipments
            {
                Shipments = new List<Shipment> { shipment },
            };

            batch = await Client.Batch.AddShipments(batch.Id, addShipmentsParameters);

            Assert.Equal(1, batch.NumShipments);
        }

        // Shipments have to exist before they can be added to a batch, so we don't need to test adding shipment via shipment creation parameters, since it's not possible.

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            BetaFeatures.Parameters.Batches.Create batchParameters = new BetaFeatures.Parameters.Batches.Create
            {
                Shipments = new List<IShipmentParameter> { shipmentParameters },
            };

            Batch batch = await Client.Batch.Create(batchParameters);

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await Client.Batch.Buy(batch.Id);

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            BetaFeatures.Parameters.Batches.GenerateLabel generateLabelParameters = new BetaFeatures.Parameters.Batches.GenerateLabel
            {
                FileFormat = "ZPL",
            };

            await Client.Batch.GenerateLabel(batch.Id, generateLabelParameters);

            // We can't assert anything meaningful here because the label gets queued for generation and may not be immediately available
            Assert.IsType<Batch>(batch);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateScanForm()
        {
            UseVCR("generate_scan_form");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            BetaFeatures.Parameters.Batches.Create batchParameters = new BetaFeatures.Parameters.Batches.Create
            {
                Shipments = new List<IShipmentParameter> { shipmentParameters },
            };

            Batch batch = await Client.Batch.Create(batchParameters);

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            batch = await Client.Batch.Buy(batch.Id);

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            BetaFeatures.Parameters.Batches.GenerateScanForm generateScanFormParameters = new BetaFeatures.Parameters.Batches.GenerateScanForm
            {
                FileFormat = "ZPL",
            };

            await Client.Batch.GenerateScanForm(batch.Id, generateScanFormParameters);

            // We can't assert anything meaningful here because the scanform gets queued for generation and may not be immediately available
            Assert.IsType<Batch>(batch);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRemoveShipments()
        {
            UseVCR("remove_shipments");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            BetaFeatures.Parameters.Batches.Create batchParameters = new BetaFeatures.Parameters.Batches.Create
            {
                Shipments = new List<IShipmentParameter> { shipment },
            };

            Batch batch = await Client.Batch.Create(batchParameters);

            Assert.Equal(1, batch.NumShipments);

            if (IsRecording()) // Yes, this is needed. Otherwise, the API says we can't modify a batch while it's being created.
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            BetaFeatures.Parameters.Batches.RemoveShipments removeShipmentsParameters = new BetaFeatures.Parameters.Batches.RemoveShipments
            {
                Shipments = new List<Shipment> { shipment },
            };

            batch = await Client.Batch.RemoveShipments(batch.Id, removeShipmentsParameters);

            Assert.Equal(0, batch.NumShipments);
        }

        // Shipments have to exist before they can be added/removed to a batch, so we don't need to test removing shipment via shipment creation parameters, since it's not possible.

        #endregion

        #endregion
    }
}
