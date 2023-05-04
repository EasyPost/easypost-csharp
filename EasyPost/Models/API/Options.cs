using System;
using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Options : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("additional_handling")]
        public bool? AdditionalHandling { get; internal set; }
        [JsonProperty("address_validation_level")]
        public string? AddressValidationLevel { get; internal set; }
        [JsonProperty("alcohol")]
        public bool? Alcohol { get; internal set; }
        [JsonProperty("billing_ref")]
        public string? BillingRef { get; internal set; }
        [JsonProperty("bill_receiver_account")]
        public string? BillReceiverAccount { get; internal set; }
        [JsonProperty("bill_receiver_postal_code")]
        public string? BillReceiverPostalCode { get; internal set; }
        [JsonProperty("bill_third_party_account")]
        public string? BillThirdPartyAccount { get; internal set; }
        [JsonProperty("bill_third_party_country")]
        public string? BillThirdPartyCountry { get; internal set; }
        [JsonProperty("bill_third_party_postal_code")]
        public string? BillThirdPartyPostalCode { get; internal set; }
        [JsonProperty("by_drone")]
        public bool? ByDrone { get; internal set; }
        [JsonProperty("carbon_neutral")]
        public bool? CarbonNeutral { get; internal set; }
        [JsonProperty("carrier_insurance_amount")]
        public string? CarrierInsuranceAmount { get; internal set; }
        [JsonProperty("carrier_notification_email")]
        public string? CarrierNotificationEmail { get; internal set; }
        [JsonProperty("carrier_notification_sms")]
        public string? CarrierNotificationSms { get; internal set; }
        [JsonProperty("certified_mail")]
        public bool? CertifiedMail { get; internal set; }
        [JsonProperty("cod_address_id")]
        public string? CodAddressId { get; internal set; }
        [JsonProperty("cod_amount")]
        public string? CodAmount { get; internal set; }
        [JsonProperty("cod_method")]
        public string? CodMethod { get; internal set; }
        [JsonProperty("commercial_invoice_format")]
        public string? CommercialInvoiceFormat { get; internal set; }
        [JsonProperty("commercial_invoice_letterhead")]
        public string? CommercialInvoiceLetterhead { get; internal set; }
        [JsonProperty("commercial_invoice_signature")]
        public string? CommercialInvoiceSignature { get; internal set; }
        [JsonProperty("commercial_invoice_size")]
        public string? CommercialInvoiceSize { get; internal set; }
        [JsonProperty("cost_center")]
        public string? CostCenter { get; internal set; }
        [JsonProperty("currency")]
        public string? Currency { get; internal set; }
        [JsonProperty("customs_broker_address_id")]
        public string? CustomsBrokerAddressId { get; internal set; }
        [JsonProperty("customs_include_shipping")]
        public string? CustomsIncludeShipping { get; internal set; }
        [JsonProperty("declared_value")]
        public double? DeclaredValue { get; internal set; }
        [JsonProperty("delivered_duty_paid")]
        public bool? DeliveredDutyPaid { get; internal set; }
        [JsonProperty("delivery_confirmation")]
        public string? DeliveryConfirmation { get; internal set; }
        [JsonProperty("delivery_time_preference")]
        public string? DeliveryTimePreference { get; internal set; }
        [JsonProperty("delivery_max_datetime")]
        public string? DeliveryMaxDatetime { get; internal set; }
        [JsonProperty("dropoff_max_datetime")]
        public DateTime? DropoffMaxDatetime { get; internal set; }
        [JsonProperty("dropoff_type")]
        public string? DropoffType { get; internal set; }
        [JsonProperty("dry_ice")]
        public bool? DryIce { get; internal set; }
        [JsonProperty("dry_ice_medical")]
        public string? DryIceMedical { get; internal set; }
        [JsonProperty("dry_ice_weight")]
        public string? DryIceWeight { get; internal set; }
        [JsonProperty("duty_payment")]
        public Dictionary<string, object?>? DutyPayment { get; internal set; }
        [JsonProperty("duty_payment_account")]
        public string? DutyPaymentAccount { get; internal set; }
        [JsonProperty("endorsement")]
        public string? Endorsement { get; internal set; }
        [JsonProperty("end_shipper_id")]
        public string? EndShipperId { get; internal set; }
        [JsonProperty("freight_charge")]
        public string? FreightCharge { get; internal set; }
        [JsonProperty("group")]
        public string? Group { get; internal set; }
        [JsonProperty("handling_instructions")]
        public string? HandlingInstructions { get; internal set; }
        [JsonProperty("hazmat")]
        public string? Hazmat { get; internal set; }
        [JsonProperty("hold_for_pickup")]
        public bool? HoldForPickup { get; internal set; }
        [JsonProperty("image_format")]
        public string? ImageFormat { get; internal set; }
        [JsonProperty("importer_address_id")]
        public string? ImporterAddressId { get; internal set; }
        [JsonProperty("import_federal_tax_id")]
        public string? ImportFederalTaxId { get; internal set; }
        [JsonProperty("import_state_tax_id")]
        public string? ImportStateTaxId { get; internal set; }
        [JsonProperty("incoterm")]
        public string? Incoterm { get; internal set; }
        [JsonProperty("invoice_number")]
        public string? InvoiceNumber { get; internal set; }
        [JsonProperty("label_date")]
        public DateTime? LabelDate { get; internal set; }
        [JsonProperty("label_format")]
        public string? LabelFormat { get; internal set; }
        [JsonProperty("label_size")]
        public string? LabelSize { get; internal set; }
        [JsonProperty("license_number")]
        public string? LicenseNumber { get; internal set; }
        [JsonProperty("machinable")]
        public string? Machinable { get; internal set; }
        [JsonProperty("neutral_delivery")]
        public bool? NeutralDelivery { get; internal set; }
        [JsonProperty("non_contact")]
        public bool? NonContract { get; internal set; }
        [JsonProperty("overlabel_construct_code")]
        public string? OverlabelConstructCode { get; internal set; }
        [JsonProperty("overlabel_construct_tracking_number")]
        public string? OverlabelOriginalTrackingNumber { get; internal set; }
        [JsonProperty("parties_to_transaction_are_related")]
        public string? PartiesToTransactionAreRelated { get; internal set; }
        [JsonProperty("payment")]
        public Dictionary<string, object>? Payment { get; internal set; }
        [JsonProperty("peel_and_return")]
        public bool? PeelAndReturn { get; internal set; }
        [JsonProperty("pickup_max_datetime")]
        public DateTime? PickupMaxDatetime { get; internal set; }
        [JsonProperty("pickup_min_datetime")]
        public DateTime? PickupMinDatetime { get; internal set; }
        [JsonProperty("po_sort")]
        public string? PoSort { get; internal set; }
        [JsonProperty("postage_label_inline")]
        public bool? PostageLabelInline { get; internal set; }
        [JsonProperty("print_custom")]
        public List<Dictionary<string, object>>? PrintCustom { get; internal set; }
        [JsonProperty("print_custom_1")]
        public string? PrintCustom1 { get; internal set; }
        [JsonProperty("print_custom_1_barcode")]
        public bool? PrintCustom1Barcode { get; internal set; }
        [JsonProperty("print_custom_1_code")]
        public string? PrintCustom1Code { get; internal set; }
        [JsonProperty("print_custom_2")]
        public string? PrintCustom2 { get; internal set; }
        [JsonProperty("print_custom_2_barcode")]
        public bool? PrintCustom2Barcode { get; internal set; }
        [JsonProperty("print_custom_2_code")]
        public string? PrintCustom2Code { get; internal set; }
        [JsonProperty("print_custom_3")]
        public string? PrintCustom3 { get; internal set; }
        [JsonProperty("print_custom_3_barcode")]
        public bool? PrintCustom3Barcode { get; internal set; }
        [JsonProperty("print_custom_3_code")]
        public string? PrintCustom3Code { get; internal set; }
        [JsonProperty("print_rate")]
        public bool? PrintRate { get; internal set; }
        [JsonProperty("receiver_liquor_license")]
        public string? ReceiverLiquorLicense { get; internal set; }
        [JsonProperty("registered_mail")]
        public bool? RegisteredMail { get; internal set; }
        [JsonProperty("registered_mail_amount")]
        public double? RegisteredMailAmount { get; internal set; }
        [JsonProperty("return_receipt")]
        public bool? ReturnReceipt { get; internal set; }
        [JsonProperty("return_service")]
        public string? ReturnService { get; internal set; }
        [JsonProperty("saturday_delivery")]
        public bool? SaturdayDelivery { get; internal set; }
        [JsonProperty("settlement_method")]
        public string? SettlementMethod { get; internal set; }
        [JsonProperty("smartpost_hub")]
        public string? SmartpostHub { get; internal set; }
        [JsonProperty("smartpost_manifest")]
        public string? SmartpostManifest { get; internal set; }
        [JsonProperty("special_rates_eligibility")]
        public string? SpecialRatesEligibility { get; internal set; }
        [JsonProperty("suppress_etd")]
        public bool? SuppressEtd { get; internal set; }

        #endregion

        public Options()
        {
        }
    }
}
