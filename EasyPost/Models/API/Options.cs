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
        public bool? AdditionalHandling { get; set; }
        [JsonProperty("address_validation_level")]
        public string? AddressValidationLevel { get; set; }
        [JsonProperty("alcohol")]
        public bool? Alcohol { get; set; }
        [JsonProperty("billing_ref")]
        public string? BillingRef { get; set; }
        [JsonProperty("bill_receiver_account")]
        public string? BillReceiverAccount { get; set; }
        [JsonProperty("bill_receiver_postal_code")]
        public string? BillReceiverPostalCode { get; set; }
        [JsonProperty("bill_third_party_account")]
        public string? BillThirdPartyAccount { get; set; }
        [JsonProperty("bill_third_party_country")]
        public string? BillThirdPartyCountry { get; set; }
        [JsonProperty("bill_third_party_postal_code")]
        public string? BillThirdPartyPostalCode { get; set; }
        [JsonProperty("by_drone")]
        public bool? ByDrone { get; set; }
        [JsonProperty("carbon_neutral")]
        public bool? CarbonNeutral { get; set; }
        [JsonProperty("carrier_insurance_amount")]
        public string? CarrierInsuranceAmount { get; set; }
        [JsonProperty("carrier_notification_email")]
        public string? CarrierNotificationEmail { get; set; }
        [JsonProperty("carrier_notification_sms")]
        public string? CarrierNotificationSms { get; set; }
        [JsonProperty("certified_mail")]
        public bool? CertifiedMail { get; set; }
        [JsonProperty("cod_address_id")]
        public string? CodAddressId { get; set; }
        [JsonProperty("cod_amount")]
        public string? CodAmount { get; set; }
        [JsonProperty("cod_method")]
        public string? CodMethod { get; set; }
        [JsonProperty("commercial_invoice_format")]
        public string? CommercialInvoiceFormat { get; set; }
        [JsonProperty("commercial_invoice_letterhead")]
        public string? CommercialInvoiceLetterhead { get; set; }
        [JsonProperty("commercial_invoice_signature")]
        public string? CommercialInvoiceSignature { get; set; }
        [JsonProperty("commercial_invoice_size")]
        public string? CommercialInvoiceSize { get; set; }
        [JsonProperty("cost_center")]
        public string? CostCenter { get; set; }
        [JsonProperty("currency")]
        public string? Currency { get; set; }
        [JsonProperty("customs_broker_address_id")]
        public string? CustomsBrokerAddressId { get; set; }
        [JsonProperty("customs_include_shipping")]
        public string? CustomsIncludeShipping { get; set; }
        [JsonProperty("declared_value")]
        public double? DeclaredValue { get; set; }
        [JsonProperty("delivered_duty_paid")]
        public bool? DeliveredDutyPaid { get; set; }
        [JsonProperty("delivery_confirmation")]
        public string? DeliveryConfirmation { get; set; }
        [JsonProperty("delivery_time_preference")]
        public string? DeliveryTimePreference { get; set; }
        [JsonProperty("dropoff_type")]
        public string? DropoffType { get; set; }
        [JsonProperty("dry_ice")]
        public bool? DryIce { get; set; }
        [JsonProperty("dry_ice_medical")]
        public string? DryIceMedical { get; set; }
        [JsonProperty("dry_ice_weight")]
        public string? DryIceWeight { get; set; }
        [JsonProperty("duty_payment_account")]
        public string? DutyPaymentAccount { get; set; }
        [JsonProperty("endorsement")]
        public string? Endorsement { get; set; }
        [JsonProperty("freight_charge")]
        public string? FreightCharge { get; set; }
        [JsonProperty("group")]
        public string? Group { get; set; }
        [JsonProperty("handling_instructions")]
        public string? HandlingInstructions { get; set; }
        [JsonProperty("hazmat")]
        public string? Hazmat { get; set; }
        [JsonProperty("hold_for_pickup")]
        public bool? HoldForPickup { get; set; }
        [JsonProperty("image_format")]
        public string? ImageFormat { get; set; }
        [JsonProperty("importer_address_id")]
        public string? ImporterAddressId { get; set; }
        [JsonProperty("import_federal_tax_id")]
        public string? ImportFederalTaxId { get; set; }
        [JsonProperty("import_state_tax_id")]
        public string? ImportStateTaxId { get; set; }
        [JsonProperty("incoterm")]
        public string? Incoterm { get; set; }
        [JsonProperty("invoice_number")]
        public string? InvoiceNumber { get; set; }
        [JsonProperty("label_date")]
        public DateTime? LabelDate { get; set; }
        [JsonProperty("label_format")]
        public string? LabelFormat { get; set; }
        [JsonProperty("label_size")]
        public string? LabelSize { get; set; }
        [JsonProperty("license_number")]
        public string? LicenseNumber { get; set; }
        [JsonProperty("machinable")]
        public string? Machinable { get; set; }
        [JsonProperty("neutral_delivery")]
        public bool? NeutralDelivery { get; set; }
        [JsonProperty("non_contact")]
        public bool? NonContract { get; set; }
        [JsonProperty("overlabel_construct_code")]
        public string? OverlabelConstructCode { get; set; }
        [JsonProperty("overlabel_construct_tracking_number")]
        public string? OverlabelOriginalTrackingNumber { get; set; }
        [JsonProperty("parties_to_transaction_are_related")]
        public string? PartiesToTransactionAreRelated { get; set; }
        [JsonProperty("payment")]
        public Dictionary<string, object>? Payment { get; set; }
        [JsonProperty("peel_and_return")]
        public bool PeelAndReturn { get; set; }
        [JsonProperty("pickup_min_datetime")]
        public DateTime? PickupMinDatetime { get; set; }
        [JsonProperty("po_sort")]
        public string? PoSort { get; set; }
        [JsonProperty("postage_label_inline")]
        public bool? PostageLabelInline { get; set; }
        [JsonProperty("print_custom")]
        public List<Dictionary<string, object>>? PrintCustom { get; set; }
        [JsonProperty("print_custom_1")]
        public string? PrintCustom1 { get; set; }
        [JsonProperty("print_custom_1_barcode")]
        public bool? PrintCustom1Barcode { get; set; }
        [JsonProperty("print_custom_1_code")]
        public string? PrintCustom1Code { get; set; }
        [JsonProperty("print_custom_2")]
        public string? PrintCustom2 { get; set; }
        [JsonProperty("print_custom_2_barcode")]
        public bool? PrintCustom2Barcode { get; set; }
        [JsonProperty("print_custom_2_code")]
        public string? PrintCustom2Code { get; set; }
        [JsonProperty("print_custom_3")]
        public string? PrintCustom3 { get; set; }
        [JsonProperty("print_custom_3_barcode")]
        public bool? PrintCustom3Barcode { get; set; }
        [JsonProperty("print_custom_3_code")]
        public string? PrintCustom3Code { get; set; }
        [JsonProperty("print_rate")]
        public bool? PrintRate { get; set; }
        [JsonProperty("receiver_liquor_license")]
        public string? ReceiverLiquorLicense { get; set; }
        [JsonProperty("registered_mail")]
        public bool? RegisteredMail { get; set; }
        [JsonProperty("registered_mail_amount")]
        public double? RegisteredMailAmount { get; set; }
        [JsonProperty("return_receipt")]
        public bool? ReturnReceipt { get; set; }
        [JsonProperty("return_service")]
        public string? ReturnService { get; set; }
        [JsonProperty("saturday_delivery")]
        public bool? SaturdayDelivery { get; set; }
        [JsonProperty("settlement_method")]
        public string? SettlementMethod { get; set; }
        [JsonProperty("smartpost_hub")]
        public string? SmartpostHub { get; set; }
        [JsonProperty("smartpost_manifest")]
        public string? SmartpostManifest { get; set; }
        [JsonProperty("special_rates_eligibility")]
        public string? SpecialRatesEligibility { get; set; }
        [JsonProperty("suppress_etd")]
        public bool? SuppressEtd { get; set; }

        #endregion
    }
}
