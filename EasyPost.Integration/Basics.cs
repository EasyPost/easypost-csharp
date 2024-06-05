using EasyPost.Integration.Utilities.Attributes;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Integration;

public class Basics
{
    /// <summary>
    ///     Test that an end-user can locally construct a response object without needing to call the API (response objects have public constructors).
    ///     If this test can be compiled, then the response objects have public constructors.
    /// </summary>
    [Fact, Testing.Access, Testing.Compile]
    public void UserCanLocallyConstructResponseObject()
    {
        // ReSharper disable once UnusedVariable
        var address = new Address();
        var addressCollection = new AddressCollection();
        var apiKey = new ApiKey();
        var apiKeyCollection = new ApiKeyCollection();
        var batch = new Batch();
        var batchCollection = new BatchCollection();
        var batchShipment = new BatchShipment();
        var brand = new Brand();
        var carrier = new Carrier();
        var carrierAccount = new CarrierAccount();
        var carrierDetail = new CarrierDetail();
        var carrierField = new CarrierField();
        var carrierFields = new CarrierFields();
        var carrierMetadata = new CarrierMetadata();
        var carrierType = new CarrierType();
        var customsInfo = new CustomsInfo();
        var customsItem = new CustomsItem();
        var endShipper = new EndShipper();
        var endShipperCollection = new EndShipperCollection();
        var error = new Error();
        var @event = new Event();
        var eventCollection = new EventCollection();
        var fee = new Fee();
        var form = new Form();
        var insurance = new Insurance();
        var insuranceCollection = new InsuranceCollection();
        var message = new Message();
        var options = new Options();
        var order = new Order();
        var parcel = new Parcel();
        var payload = new Payload();
        var paymentMethod = new PaymentMethod();
        var paymentMethodsSummary = new PaymentMethodsSummary();
        var paymentRefundBeta = new Models.API.Beta.PaymentRefund();
        var pickup = new Pickup();
        var pickupCollection = new PickupCollection();
        var pickupRate = new PickupRate();
        var postageLabel = new PostageLabel();
        var predefinedPackage = new PredefinedPackage();
        var rate = new Rate();
        var rateWithEstimatedDeliveryDate = new RateWithEstimatedDeliveryDate();
        var rateWithTimeInTransitDetailsByShipDate = new RateWithTimeInTransitDetailsByShipDate();
        var rateWithTimeInTransitDetailsByDeliveryDate = new RateWithTimeInTransitDetailsByDeliveryDate();
        var referralCustomer = new ReferralCustomer();
        var refund = new Refund();
        var report = new Report();
        var reportCollection = new ReportCollection();
        var scanForm = new ScanForm();
        var scanFormCollection = new ScanFormCollection();
        var serviceLevel = new ServiceLevel();
        var shipment = new Shipment();
        var shipmentCollection = new ShipmentCollection();
        var shipmentOption = new ShipmentOption();
        var smartRate = new SmartRate();
        var statelessRateBeta = new Models.API.Beta.StatelessRate();
        var supportedFeature = new SupportedFeature();
        var taxIdentifier = new TaxIdentifier();
        var timeInTransit = new TimeInTransit();
        var timeInTransitDetails = new TimeInTransitDetails();
        var timeInTransitDetailsByShipDate = new TimeInTransitDetailsByShipDate();
        var timeInTransitDetailsByDeliveryDate = new TimeInTransitDetailsByDeliveryDate();
        var tracker = new Tracker();
        var trackerCollection = new TrackerCollection();
        var trackingDetail = new TrackingDetail();
        var trackingLocation = new TrackingLocation();
        var user = new User();
        var verification = new Verification();
        var verificationDetails = new VerificationDetails();
        var verifications = new Verifications();
        var webhook = new Webhook();
    }

    /// <summary>
    ///     Test that an end-user can locally construct a parameter object (parameter objects have public constructors).
    ///     If this test can be compiled, then the parameter objects have public constructors.
    /// </summary>
    [Fact, Testing.Access, Testing.Compile]
    public void UserCanConstructParameterSets()
    {
        var addressCreateParameters = new EasyPost.Parameters.Address.Create();
        var addressAllParameters = new EasyPost.Parameters.Address.All();
        var batchCreateParameters = new EasyPost.Parameters.Batch.Create();
        var batchAllParameters = new EasyPost.Parameters.Batch.All();
        var batchAddShipmentsParameters = new EasyPost.Parameters.Batch.AddShipments();
        var batchRemoveShipmentsParameters = new EasyPost.Parameters.Batch.RemoveShipments();
        var batchGenerateLabelParameters = new EasyPost.Parameters.Batch.GenerateLabel();
        var batchGenerateScanFormParameters = new EasyPost.Parameters.Batch.GenerateScanForm();
        var betaRateRetrieveParameters = new EasyPost.Parameters.Beta.Rate.Retrieve();
        var carrierAccountCreateParameters = new EasyPost.Parameters.CarrierAccount.Create("CarrierNameAccount");
        var carrierAccountCreateFedExParameters = new EasyPost.Parameters.CarrierAccount.CreateFedEx();
        var carrierAccountCreateFedExSmartPostParameters = new EasyPost.Parameters.CarrierAccount.CreateFedExSmartPost();
        var carrierAccountCreateUpsParameters = new EasyPost.Parameters.CarrierAccount.CreateUps();
        var carrierAccountUpdateParameters = new EasyPost.Parameters.CarrierAccount.Update();
        var carrierMetadataRetrieveParameters = new EasyPost.Parameters.CarrierMetadata.Retrieve();
        var customsInfoCreateParameters = new EasyPost.Parameters.CustomsInfo.Create();
        var customsItemCreateParameters = new EasyPost.Parameters.CustomsItem.Create();
        var endShipperCreateParameters = new EasyPost.Parameters.EndShipper.Create();
        var endShipperAllParameters = new EasyPost.Parameters.EndShipper.All();
        var endShipperUpdateParameters = new EasyPost.Parameters.EndShipper.Update();
        var eventAllParameters = new EasyPost.Parameters.Event.All();
        var insuranceCreateParameters = new EasyPost.Parameters.Insurance.Create();
        var insuranceAllParameters = new EasyPost.Parameters.Insurance.All();
        var orderCreateParameters = new EasyPost.Parameters.Order.Create();
        var orderBuyParametersWithCarrierAndService = new EasyPost.Parameters.Order.Buy("carrier", "service");
        var orderBuyParametersWithRate = new EasyPost.Parameters.Order.Buy(new Rate());
        var parcelCreateParameters = new EasyPost.Parameters.Parcel.Create();
        var pickupCreateParameters = new EasyPost.Parameters.Pickup.Create();
        var pickupAllParameters = new EasyPost.Parameters.Pickup.All();
        var pickupBuyParametersWithCarrierAndService = new EasyPost.Parameters.Pickup.Buy("carrier", "service");
        var pickupBuyParametersWithRate = new EasyPost.Parameters.Pickup.Buy(new Rate());
        var referralCustomerCreateParameters = new EasyPost.Parameters.ReferralCustomer.CreateReferralCustomer();
        var referralCustomerAllParameters = new EasyPost.Parameters.ReferralCustomer.All();
        var referralCustomerAddPaymentMethodParameters = new EasyPost.Parameters.ReferralCustomer.AddPaymentMethod();
        var referralCustomerRefundByAmountParameters = new EasyPost.Parameters.ReferralCustomer.RefundByAmount();
        var referralCustomerRefundByPaymentLogParameters = new EasyPost.Parameters.ReferralCustomer.RefundByPaymentLog();
        var refundCreateParameters = new EasyPost.Parameters.Refund.Create();
        var refundAllParameters = new EasyPost.Parameters.Refund.All();
        var reportCreateParameters = new EasyPost.Parameters.Report.Create();
        var reportAllParameters = new EasyPost.Parameters.Report.All();
        var scanFormCreateParameters = new EasyPost.Parameters.ScanForm.Create();
        var scanFormAllParameters = new EasyPost.Parameters.ScanForm.All();
        var shipmentCreateParameters = new EasyPost.Parameters.Shipment.Create();
        var shipmentAllParameters = new EasyPost.Parameters.Shipment.All();
        var shipmentBuyParametersWithCarrierAndServiceWithRate = new EasyPost.Parameters.Shipment.Buy(new Rate());
        var shipmentBuyParametersWithCarrierAndServiceWithRateId = new EasyPost.Parameters.Shipment.Buy("rate_id");
        var shipmentGenerateFormParameters = new EasyPost.Parameters.Shipment.GenerateForm();
        var shipmentGenerateLabelParameters = new EasyPost.Parameters.Shipment.GenerateLabel();
        var shipmentInsureParameters = new EasyPost.Parameters.Shipment.Insure();
        var shipmentRegenerateRatesParameters = new EasyPost.Parameters.Shipment.RegenerateRates();
        var shipmentRetrieveEstimatedDeliveryDateParameters = new EasyPost.Parameters.Shipment.RetrieveEstimatedDeliveryDate();
        var smartRateEstimateDeliveryDateByShipDateParameters = new EasyPost.Parameters.SmartRate.EstimateDeliveryDateByShipDate();
        var smartRateRecommendShipDateByDeliveryDateParameters = new EasyPost.Parameters.SmartRate.RecommendShipDateByDeliveryDate();
        var taxIdentifierCreateParameters = new EasyPost.Parameters.TaxIdentifier.Create();
        var trackerCreateParameters = new EasyPost.Parameters.Tracker.Create();
        var trackerAllParameters = new EasyPost.Parameters.Tracker.All();
        var trackerCreateListParameters = new EasyPost.Parameters.Tracker.CreateList();
        var userCreateChildParameters = new EasyPost.Parameters.User.CreateChild();
        var userAllChildrenParameters = new EasyPost.Parameters.User.AllChildren();
        var userUpdateParameters = new EasyPost.Parameters.User.Update();
        var userUpdateBrandParameters = new EasyPost.Parameters.User.UpdateBrand();
        var webhookCreateParameters = new EasyPost.Parameters.Webhook.Create();
        var webhookAllParameters = new EasyPost.Parameters.Webhook.All();
        var webhookUpdateParameters = new EasyPost.Parameters.Webhook.Update();
    }

    /// <summary>
    ///     Test that an end-user can locally construct an object and set its ID.
    /// </summary>
    [Fact, Testing.Access, Testing.Compile]
    public void UserCanSetObjectId()
    {
        // Construct a local object, setting its ID
        var address = new Address { Id = "some_id" };

        // Assert that the ID was set
        Assert.Equal("some_id", address.Id);
    }

    /// <summary>
    ///     Test that an end-user can locally construct all available hooks.
    ///     If this test can be compiled, then the hooks are publicly accessible.
    /// </summary>
    [Fact, Testing.Access, Testing.Compile]
    public void UserCanCreateHooks()
    {
        // Can set up each hook event handler during construction
        var hooks = new Hooks()
        {
            OnRequestResponseReceived = new EventHandler<OnRequestResponseReceivedEventArgs>((sender, args) =>
            {
                Console.WriteLine(args.Method);
                Console.WriteLine(args.Uri);
                Console.WriteLine(args.Headers);
                Console.WriteLine(args.StatusCode);
                Console.WriteLine(args.ResponseBody);
                Console.WriteLine(args.RequestTimestamp);
                Console.WriteLine(args.ResponseTimestamp);
                Console.WriteLine(args.Id);
            }),
            OnRequestExecuting = new EventHandler<OnRequestExecutingEventArgs>((sender, args) =>
            {
                Console.WriteLine(args.Method);
                Console.WriteLine(args.Uri);
                Console.WriteLine(args.Headers);
                Console.WriteLine(args.RequestBody);
                Console.WriteLine(args.RequestTimestamp);
                Console.WriteLine(args.Id);
            }),
        };

        // Can set up/add to each hook event handler after construction
        hooks.OnRequestResponseReceived += (sender, args) =>
        {
            Console.WriteLine(args.Method);
            Console.WriteLine(args.Uri);
            Console.WriteLine(args.Headers);
            Console.WriteLine(args.StatusCode);
            Console.WriteLine(args.ResponseBody);
            Console.WriteLine(args.RequestTimestamp);
            Console.WriteLine(args.ResponseTimestamp);
            Console.WriteLine(args.Id);
        };

        hooks.OnRequestExecuting += (sender, args) =>
        {
            Console.WriteLine(args.Method);
            Console.WriteLine(args.Uri);
            Console.WriteLine(args.Headers);
            Console.WriteLine(args.RequestBody);
            Console.WriteLine(args.RequestTimestamp);
            Console.WriteLine(args.Id);
        };

        // Can pass hooks to a client during construction
        var client = new Client(new ClientConfiguration("not-a-real-api-key")
        {
            Hooks = hooks,
        });

        // Cannot edit hooks via a constructed client
    }
}
