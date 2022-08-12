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
        public bool? additional_handling { get; set; }
        [JsonProperty("address_validation_level")]
        public string address_validation_level { get; set; }
        [JsonProperty("alcohol")]
        public bool? alcohol { get; set; }
        [JsonProperty("bill_receiver_account")]
        public string bill_receiver_account { get; set; }
        [JsonProperty("bill_receiver_postal_code")]
        public string bill_receiver_postal_code { get; set; }
        [JsonProperty("bill_third_party_account")]
        public string bill_third_party_account { get; set; }
        [JsonProperty("bill_third_party_country")]
        public string bill_third_party_country { get; set; }
        [JsonProperty("bill_third_party_postal_code")]
        public string bill_third_party_postal_code { get; set; }
        [JsonProperty("by_drone")]
        public bool? by_drone { get; set; }
        [JsonProperty("carbon_neutral")]
        public bool? carbon_neutral { get; set; }
        [JsonProperty("carrier_insurance_amount")]
        public string carrier_insurance_amount { get; set; }
        [JsonProperty("carrier_notification_email")]
        public string carrier_notification_email { get; set; }
        [JsonProperty("carrier_notification_sms")]
        public string carrier_notification_sms { get; set; }
        [JsonProperty("certified_mail")]
        public bool? certified_mail { get; set; }
        [JsonProperty("cod_address_id")]
        public string cod_address_id { get; set; }
        [JsonProperty("cod_amount")]
        public string cod_amount { get; set; }
        [JsonProperty("cod_method")]
        public string cod_method { get; set; }
        [JsonProperty("commercial_invoice_format")]
        public string commercial_invoice_format { get; set; }
        [JsonProperty("commercial_invoice_letterhead")]
        public string commercial_invoice_letterhead { get; set; }
        [JsonProperty("commercial_invoice_signature")]
        public string commercial_invoice_signature { get; set; }
        [JsonProperty("commercial_invoice_size")]
        public string commercial_invoice_size { get; set; }
        [JsonProperty("cost_center")]
        public string cost_center { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("customs_broker_address_id")]
        public string customs_broker_address_id { get; set; }
        [JsonProperty("customs_include_shipping")]
        public string customs_include_shipping { get; set; }
        [JsonProperty("declared_value")]
        public double? declared_value { get; set; }
        [JsonProperty("delivered_duty_paid")]
        public bool? delivered_duty_paid { get; set; }
        [JsonProperty("delivery_confirmation")]
        public string delivery_confirmation { get; set; }
        [JsonProperty("delivery_time_preference")]
        public string delivery_time_preference { get; set; }
        [JsonProperty("dropoff_type")]
        public string dropoff_type { get; set; }
        [JsonProperty("dry_ice")]
        public bool? dry_ice { get; set; }
        [JsonProperty("dry_ice_medical")]
        public string dry_ice_medical { get; set; }
        [JsonProperty("dry_ice_weight")]
        public string dry_ice_weight { get; set; }
        [JsonProperty("duty_payment_account")]
        public string duty_payment_account { get; set; }
        [JsonProperty("endorsement")]
        public string endorsement { get; set; }
        [JsonProperty("freight_charge")]
        public string freight_charge { get; set; }
        [JsonProperty("group")]
        public string group { get; set; }
        [JsonProperty("handling_instructions")]
        public string handling_instructions { get; set; }
        [JsonProperty("hazmat")]
        public string hazmat { get; set; }
        [JsonProperty("hold_for_pickup")]
        public bool? hold_for_pickup { get; set; }
        [JsonProperty("image_format")]
        public string image_format { get; set; }
        [JsonProperty("import_federal_tax_id")]
        public string import_federal_tax_id { get; set; }
        [JsonProperty("import_state_tax_id")]
        public string import_state_tax_id { get; set; }
        [JsonProperty("importer_address_id")]
        public string importer_address_id { get; set; }
        [JsonProperty("incoterm")]
        public string incoterm { get; set; }
        [JsonProperty("invoice_number")]
        public string invoice_number { get; set; }
        [JsonProperty("label_date")]
        public DateTime? label_date { get; set; }
        [JsonProperty("label_format")]
        public string label_format { get; set; }
        [JsonProperty("label_size")]
        public string label_size { get; set; }
        [JsonProperty("license_number")]
        public string license_number { get; set; }
        [JsonProperty("machinable")]
        public string machinable { get; set; }
        [JsonProperty("neutral_delivery")]
        public bool? neutral_delivery { get; set; }
        [JsonProperty("non_contact")]
        public bool? non_contract { get; set; }
        [JsonProperty("overlabel_construct_code")]
        public string overlabel_construct_code { get; set; }
        [JsonProperty("overlabel_construct_tracking_number")]
        public string overlabel_original_tracking_number { get; set; }
        [JsonProperty("parties_to_transaction_are_related")]
        public string parties_to_transaction_are_related { get; set; }
        [JsonProperty("payment")]
        public Dictionary<string, object> payment { get; set; }
        [JsonProperty("peel_and_return")]
        public bool peel_and_return { get; set; }
        [JsonProperty("pickup_min_datetime")]
        public DateTime? pickup_min_datetime { get; set; }
        [JsonProperty("po_sort")]
        public string po_sort { get; set; }
        [JsonProperty("postage_label_inline")]
        public bool? postage_label_inline { get; set; }
        [JsonProperty("print_custom")]
        public List<Dictionary<string, object?>> print_custom { get; set; }
        [JsonProperty("print_custom_1")]
        public string print_custom_1 { get; set; }
        [JsonProperty("print_custom_1_barcode")]
        public bool? print_custom_1_barcode { get; set; }
        [JsonProperty("print_custom_1_code")]
        public string print_custom_1_code { get; set; }
        [JsonProperty("print_custom_2")]
        public string print_custom_2 { get; set; }
        [JsonProperty("print_custom_2_barcode")]
        public bool? print_custom_2_barcode { get; set; }
        [JsonProperty("print_custom_2_code")]
        public string print_custom_2_code { get; set; }
        [JsonProperty("print_custom_3")]
        public string print_custom_3 { get; set; }
        [JsonProperty("print_custom_3_barcode")]
        public bool? print_custom_3_barcode { get; set; }
        [JsonProperty("print_custom_3_code")]
        public string print_custom_3_code { get; set; }
        [JsonProperty("print_rate")]
        public bool? print_rate { get; set; }
        [JsonProperty("receiver_liquor_license")]
        public string receiver_liquor_license { get; set; }
        [JsonProperty("registered_mail")]
        public bool? registered_mail { get; set; }
        [JsonProperty("registered_mail_amount")]
        public double? registered_mail_amount { get; set; }
        [JsonProperty("return_receipt")]
        public bool? return_receipt { get; set; }
        [JsonProperty("return_service")]
        public string return_service { get; set; }
        [JsonProperty("saturday_delivery")]
        public bool? saturday_delivery { get; set; }
        [JsonProperty("settlement_method")]
        public string settlement_method { get; set; }
        [JsonProperty("smartpost_hub")]
        public string smartpost_hub { get; set; }
        [JsonProperty("smartpost_manifest")]
        public string smartpost_manifest { get; set; }
        [JsonProperty("billing_ref")]
        public string billing_ref { get; set; }
        [JsonProperty("special_rates_eligibility")]
        public string special_rates_eligibility { get; set; }
        [JsonProperty("suppress_etd")]
        public bool? suppress_etd { get; set; }

        #endregion
    }
}
