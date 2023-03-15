using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
    public class BatchTests : UnitTest
    {
        // NOTE: Due to an issue EasyVCR has with multiple exact matches of the same request in the same cassette, we have to split the add/remove overloads into separate unit tests.

        public BatchTests() : base("batch_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

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

            batch = await batch.AddShipments(addShipmentsParameters);

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

            batch = await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            BetaFeatures.Parameters.Batches.GenerateLabel generateLabelParameters = new BetaFeatures.Parameters.Batches.GenerateLabel
            {
                FileFormat = "ZPL",
            };

            await batch.GenerateLabel(generateLabelParameters);

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

            batch = await batch.Buy();

            if (IsRecording())
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            BetaFeatures.Parameters.Batches.GenerateScanForm generateScanFormParameters = new BetaFeatures.Parameters.Batches.GenerateScanForm
            {
                FileFormat = "ZPL",
            };

            await batch.GenerateScanForm(generateScanFormParameters);

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

            BetaFeatures.Parameters.Batches.RemoveShipments removeShipmentsParameters = new BetaFeatures.Parameters.Batches.RemoveShipments
            {
                Shipments = new List<Shipment> { shipment },
            };

            batch = await batch.RemoveShipments(removeShipmentsParameters);

            Assert.Equal(0, batch.NumShipments);
        }

        // Shipments have to exist before they can be added/removed to a batch, so we don't need to test removing shipment via shipment creation parameters, since it's not possible.

        #endregion

        #endregion
    }
}
