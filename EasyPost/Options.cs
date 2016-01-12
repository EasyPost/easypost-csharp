using System;

namespace EasyPost {
    public class Options : IResource {
        public Nullable<bool> additional_handling { get; set; }
        public string address_validation_level { get; set; }
        public Nullable<bool> alcohol { get; set; }
        public string bill_receiver_account { get; set; }
        public string bill_receiver_postal_code { get; set; }
        public string bill_third_party_account { get; set; }
        public string bill_third_party_country { get; set; }
        public string bill_third_party_postal_code { get; set; }
        public Nullable<bool> by_drone { get; set; }
        public Nullable<bool> carbon_neutral { get; set; }
        public string carrier_insurance_amount { get; set; }
        public string cod_address_id { get; set; }
        public string cod_amount { get; set; }
        public string cod_method { get; set; }
        public string commercial_invoice_format { get; set; }
        public string commercial_invoice_size { get; set; }
        public string cost_center { get; set; }
        public string currency { get; set; }
        public Nullable<double> declared_value { get; set; }
        public Nullable<bool> delivered_duty_paid { get; set; }
        public string delivery_confirmation { get; set; }
        public string delivery_time_preference { get; set; }
        public string duty_payment_account { get; set; }
        public Nullable<bool> dry_ice { get; set; }
        public string dry_ice_medical { get; set; }
        public string dry_ice_weight { get; set; }
        public string freight_charge { get; set; }
        public string group { get; set; }
        public string handling_instructions { get; set; }
        public string hazmat { get; set; }
        public Nullable<bool> hold_for_pickup { get; set; }
        public string image_format { get; set; }
        public string invoice_number { get; set; }
        public Nullable<DateTime> label_date { get; set; }
        public string label_format { get; set; }
        public string label_size { get; set; }
        public string machinable { get; set; }
        public Nullable<bool> neutral_delivery { get; set; }
        public Nullable<bool> non_contract { get; set; }
        public string po_sort { get; set; }
        public string print_custom_1 { get; set; }
        public string print_custom_2 { get; set; }
        public string print_custom_3 { get; set; }
        public string print_custom_1_code { get; set; }
        public string print_custom_2_code { get; set; }
        public string print_custom_3_code { get; set; }
        public Nullable<bool> print_custom_1_barcode { get; set; }
        public Nullable<bool> print_custom_2_barcode { get; set; }
        public Nullable<bool> print_custom_3_barcode { get; set; }
        public Nullable<bool> print_rate { get; set; }
        public string return_service { get; set; }
        public Nullable<bool> saturday_delivery { get; set; }
        public string settlement_method { get; set; }
        public string smartpost_hub { get; set; }
        public string smartpost_manifest { get; set; }
        public string special_rates_eligibility { get; set; }
    }
}
