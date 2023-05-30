using EasyPost.Integration.Utilities.Attributes;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using Xunit;

namespace EasyPost.Integration;

public class Basics
{
    /// <summary>
    ///     Test that an end-user can locally construct a response object without needing to call the API (response objects have public constructors).
    ///     If this test can be compiled, then the response objects have public constructors.
    /// </summary>
    [Fact, Testing.Access, Testing.Compile]
    public void TestUserCanLocallyConstructResponseObject()
    {
        var address = new Address();
        var addressCollection = new AddressCollection();
        var apiKey = new ApiKey();
        var apiKeyCollection = new ApiKeyCollection();
        var batch = new Batch();
        var batchCollection = new BatchCollection();
        var batchShipment = new BatchShipment();
        var brand = new Brand();
        var carbonOffset = new CarbonOffset();
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
        var paymentRefund = new PaymentRefund();
        var pickup = new Pickup();
        var pickupCollection = new PickupCollection();
        var pickupRate = new PickupRate();
        var postageLabel = new PostageLabel();
        var predefinedPackage = new PredefinedPackage();
        var rate = new Rate();
        var rateWithEstimatedDeliveryDate = new RateWithEstimatedDeliveryDate();
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
        var statelessRate = new StatelessRate();
        var supportedFeature = new SupportedFeature();
        var taxIdentifier = new TaxIdentifier();
        var timeInTransit = new TimeInTransit();
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
}
