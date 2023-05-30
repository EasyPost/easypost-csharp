using System;
using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#options-object">EasyPost options set</a>.
    /// </summary>
    public class Options : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     When true, an additional handling fee will be applied to the shipment.
        ///     An Additional Handling charge may be applied to the following:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Any article that is encased in an outside shipping container made of metal or wood.</description>
        ///         </item>
        ///         <item>
        ///             <description>Any item, such as a barrel, drum, pail or tire, that is not fully encased in a corrugated cardboard shipping container.</description>
        ///         </item>
        ///         <item>
        ///             <description>Any package with the longest side exceeding 60 inches or its second longest side exceeding 30 inches.</description>
        ///         </item>
        ///         <item>
        ///             <description>Any package with an actual weight greater than 70 pounds.</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("additional_handling")]
        public bool? AdditionalHandling { get; set; }

        /// <summary>
        ///     Setting this option to "0", will allow the minimum amount of address information to pass the validation check. Only for USPS postage.
        /// </summary>
        [JsonProperty("address_validation_level")]
        public string? AddressValidationLevel { get; set; }

        /// <summary>
        ///     Set this option to true if your shipment contains alcohol.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>UPS</term>
        ///             <description>Only supported for US Domestic shipments.</description>
        ///         </item>
        ///         <item>
        ///             <term>FedEx</term>
        ///             <description>Only supported for US Domestic shipments.</description>
        ///         </item>
        ///         <item>
        ///             <term>Canada Post</term>
        ///             <description>
        ///                 Requires adult signature 19 years or older.
        ///                 If you want adult signature 18 years or older, instead set <see cref="DeliveryConfirmation"/> to "ADULT_SIGNATURE".
        ///             </description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("alcohol")]
        public bool? Alcohol { get; set; }

        /// <summary>
        ///     A reference ID for aggregating DHL eCommerce billing data.
        /// </summary>
        [JsonProperty("billing_ref")]
        public string? BillingRef { get; set; }

        /// <summary>
        ///     Obsolete. Use <see cref="Payment"/> instead.
        /// </summary>
        [Obsolete("Use Payment instead.")]
        [JsonProperty("bill_receiver_account")]
        public string? BillReceiverAccount { get; set; }

        /// <summary>
        ///     Obsolete. Use <see cref="Payment"/> instead.
        /// </summary>
        [Obsolete("Use Payment instead.")]
        [JsonProperty("bill_receiver_postal_code")]
        public string? BillReceiverPostalCode { get; set; }

        /// <summary>
        ///     Obsolete. Use <see cref="Payment"/> instead.
        /// </summary>
        [Obsolete("Use Payment instead.")]
        [JsonProperty("bill_third_party_account")]
        public string? BillThirdPartyAccount { get; set; }

        /// <summary>
        ///     Obsolete. Use <see cref="Payment"/> instead.
        /// </summary>
        [Obsolete("Use Payment instead.")]
        [JsonProperty("bill_third_party_country")]
        public string? BillThirdPartyCountry { get; set; }

        /// <summary>
        ///     Obsolete. Use <see cref="Payment"/> instead.
        /// </summary>
        [Obsolete("Use Payment instead.")]
        [JsonProperty("bill_third_party_postal_code")]
        public string? BillThirdPartyPostalCode { get; set; }

        /// <summary>
        ///     Setting this option to true will indicate to the carrier to prefer delivery by drone, if the carrier supports drone delivery.
        /// </summary>
        [JsonProperty("by_drone")]
        public bool? ByDrone { get; set; }

        /// <summary>
        ///     Setting this to true will add a charge to reduce carbon emissions by carriers who support this option.
        ///     Alternatively, see <see cref="CarbonOffset"/> which is provided by EasyPost and works with all carriers.
        /// </summary>
        [JsonProperty("carbon_neutral")]
        public bool? CarbonNeutral { get; set; }
        [JsonProperty("carrier_insurance_amount")]
        public string? CarrierInsuranceAmount { get; set; }
        [JsonProperty("carrier_notification_email")]
        public string? CarrierNotificationEmail { get; set; }
        [JsonProperty("carrier_notification_sms")]
        public string? CarrierNotificationSms { get; set; }

        /// <summary>
        ///     Certified Mail provides the sender with a mailing receipt and, upon request, electronic verification that an article was delivered or that a delivery attempt was made.
        /// </summary>
        [JsonProperty("certified_mail")]
        public bool? CertifiedMail { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Address"/> to which the COD payment should be returned.
        ///     Defaults to the origin address.
        ///     Only available on FedEx shipments.
        /// </summary>
        [JsonProperty("cod_address_id")]
        public string? CodAddressId { get; set; }

        /// <summary>
        ///     Adding an amount will have the carrier collect the specified amount from the recipient.
        /// </summary>
        [JsonProperty("cod_amount")]
        public string? CodAmount { get; set; }

        /// <summary>
        ///     Method for payment.
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"CASH"</description>
        ///         </item>
        ///         <item>
        ///             <description>"CHECK"</description>
        ///         </item>
        ///         <item>
        ///             <description>"MONEY_ORDER"</description>
        ///         </item>
        ///     </list>
        /// </summary>
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

        /// <summary>
        ///     A description of the content of the shipment.
        /// </summary>
        [JsonProperty("content_description")]
        public string? ContentDescription { get; set; }
        [JsonProperty("cost_center")]
        public string? CostCenter { get; set; }

        /// <summary>
        ///     Which currency this <see cref="Shipment"/> will show for rates if carrier allows.
        /// </summary>
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

        /// <summary>
        ///     If you want to request a signature, you can pass "ADULT_SIGNATURE" or "SIGNATURE".
        ///     You may also request "NO_SIGNATURE" to leave the package at the door.
        ///     "NO_SIGNATURE" is equivalent to releasing liability.
        ///     Some options may be limited for international shipments. Carrier specific delivery confirmation options are:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>FedEx</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>"INDIRECT_SIGNATURE"</term>
        ///                         <description>Requires the signature of someone at the delivery address or from somebody nearby, such as a neighbor</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>USPS</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>"ADULT_SIGNATURE_RESTRICTED"</term>
        ///                         <description>Requires the signature of the addressee only, who must be 21 years of age or older</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>"SIGNATURE_RESTRICTED"</term>
        ///                         <description>Requires the signature of the recipient or a responsible party</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Canada Post</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>"DO_NOT_SAFE_DROP"</term>
        ///                         <description>Tells the carrier to not hide the package ("safe drop")</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>GSO</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>"STANDARD_SIGNATURE"</term>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>DHL Express</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>"SIGNATURE"</term>
        ///                         <description>DHL Express Direct Signature</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>"NO_SIGNATURE"</term>
        ///                         <description>DHL Express Signature Release</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("delivery_confirmation")]
        public string? DeliveryConfirmation { get; set; }
        [JsonProperty("delivery_time_preference")]
        public string? DeliveryTimePreference { get; set; }

        /// <summary>
        ///     The earliest a package should be delivered.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("delivery_min_datetime")]
        public string? DeliveryMinDatetime { get; set; }

        /// <summary>
        ///     The latest a package should be delivered.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("delivery_max_datetime")]
        public string? DeliveryMaxDatetime { get; set; }
        [JsonProperty("dropoff_max_datetime")]
        public DateTime? DropoffMaxDatetime { get; set; }

        /// <summary>
        ///     Method the customer will use to transfer the package to the carrier.
        ///     Supported types and their corresponding carrier codes are:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>FedEx</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>"REGULAR_PICKUP"</term>
        ///                         <description>Customer to transfer package during regular pickup (default)</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>"SCHEDULED_PICKUP"</term>
        ///                         <description>Customer to transfer package during scheduled pickup</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>"RETAIL_LOCATION"</term>
        ///                         <description>Customer to transfer package at FedEx retail location</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>"STATION"</term>
        ///                         <description>"STATION"</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>"DROP_BOX"</term>
        ///                         <description>Customer to use carrier drop box for package transfer</description>
        ///                     </item>
        ///                 </list>
        ///            </description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("dropoff_type")]
        public string? DropoffType { get; set; }

        /// <summary>
        ///     Package contents contain dry ice.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>UPS</term>
        ///             <description>Needs <see cref="DryIceWeight"/> to be set.</description>
        ///         </item>
        ///         <item>
        ///             <term>UPS MailInnovations</term>
        ///             <description>Needs <see cref="DryIceWeight"/> to be set.</description>
        ///         </item>
        ///         <item>
        ///             <term>FedEx</term>
        ///             <description>Needs <see cref="DryIceWeight"/> to be set.</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("dry_ice")]
        public bool? DryIce { get; set; }

        /// <summary>
        ///     Whether the dry ice is for medical use.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>UPS</term>
        ///             <description>Needs <see cref="DryIceWeight"/> to be set.</description>
        ///         </item>
        ///         <item>
        ///             <term>UPS MailInnovations</term>
        ///             <description>Needs <see cref="DryIceWeight"/> to be set.</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("dry_ice_medical")]
        public string? DryIceMedical { get; set; }

        /// <summary>
        ///     Weight of the dry ice, in ounces.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>UPS</term>
        ///             <description>Needs <see cref="DryIce"/> to be set.</description>
        ///         </item>
        ///         <item>
        ///             <term>UPS MailInnovations</term>
        ///             <description>Needs <see cref="DryIce"/> to be set.</description>
        ///         </item>
        ///         <item>
        ///             <term>FedEx</term>
        ///             <description>Needs <see cref="DryIce"/> to be set.</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("dry_ice_weight")]
        public string? DryIceWeight { get; set; }

        /// <summary>
        ///     Set to bill the correct account for purchasing postage.
        ///     This option is only available with FedEx and UPS.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Key</term>
        ///             <description>Value</description>
        ///         </listheader>
        ///         <item>
        ///             <term>type</term>
        ///             <description>One of the following:
        ///                 <list type="bullet">
        ///                     <item>
        ///                         <description>"THIRD_PARTY"</description>
        ///                     </item>
        ///                     <item>
        ///                         <description>"RECEIVER"</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>account</term>
        ///             <description>Account number</description>
        ///         </item>
        ///         <item>
        ///             <term>country</term>
        ///             <description>Country code that the account is based in</description>
        ///         </item>
        ///         <item>
        ///             <term>postage_code</term>
        ///             <description>Postal code that the account is based in</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("duty_payment")]
        public Dictionary<string, object?>? DutyPayment { get; set; }
        [JsonProperty("duty_payment_account")]
        public string? DutyPaymentAccount { get; set; }

        /// <summary>
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"ADDRESS_SERVICE_REQUESTED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"FORWARDING_SERVICE_REQUESTED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"CHANGE_SERVICE_REQUESTED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"RETURN_SERVICE_REQUESTED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"LEAVE_IF_NO_RESPONSE"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("endorsement")]
        public string? Endorsement { get; set; }

        /// <summary>
        ///     Specify the responsible <see cref="EndShipper"/> for the shipment.
        /// </summary>
        [JsonProperty("end_shipper_id")]
        public string? EndShipperId { get; set; }

        /// <summary>
        ///     Additional cost to be added to the invoice of this <see cref="Shipment"/>.
        ///     Only applies to UPS currently.
        /// </summary>
        [JsonProperty("freight_charge")]
        public string? FreightCharge { get; set; }
        [JsonProperty("group")]
        public string? Group { get; set; }

        /// <summary>
        ///     This is to designate special instructions for the carrier, such as "Do not drop!".
        /// </summary>
        [JsonProperty("handling_instructions")]
        public string? HandlingInstructions { get; set; }

        /// <summary>
        ///     Dangerous goods indicator.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"PRIMARY_CONTAINED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"PRIMARY_PACKED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"PRIMARY"</description>
        ///         </item>
        ///         <item>
        ///             <description>"SECONDARY_CONTAINED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"SECONDARY_PACKED"</description>
        ///         </item>
        ///         <item>
        ///             <description>"SECONDARY"</description>
        ///         </item>
        ///         <item>
        ///             <description>"ORMD"</description>
        ///         </item>
        ///         <item>
        ///             <description>"LIMITED_QUANTITY"</description>
        ///         </item>
        ///         <item>
        ///             <description>"LITHIUM"</description>
        ///         </item>
        ///     </list>
        ///     Applies to USPS, FedEx and DHL eCommerce.
        /// </summary>
        [JsonProperty("hazmat")]
        public string? Hazmat { get; set; }

        /// <summary>
        ///     Package will wait at carrier facility for pickup.
        /// </summary>
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

        /// <summary>
        ///     Incoterm negotiated for shipment.
        ///     Supported values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"EXW"</description>
        ///         </item>
        ///         <item>
        ///             <description>"FCA"</description>
        ///         </item>
        ///         <item>
        ///             <description>"CPT"</description>
        ///         </item>
        ///         <item>
        ///             <description>"CIP"</description>
        ///         </item>
        ///         <item>
        ///             <description>"DAT"</description>
        ///         </item>
        ///         <item>
        ///             <description>"DAP"</description>
        ///         </item>
        ///         <item>
        ///             <description>"DDP"</description>
        ///         </item>
        ///         <item>
        ///             <description>"FAS"</description>
        ///         </item>
        ///         <item>
        ///             <description>"FOB"</description>
        ///         </item>
        ///         <item>
        ///             <description>"CFR"</description>
        ///         </item>
        ///         <item>
        ///             <description>"CIF"</description>
        ///         </item>
        ///     </list>
        ///     Setting this value to anything other than "DDP" will pass the cost and responsibility of duties on to the recipient of the package(s), as specified by Incoterms rules.
        /// </summary>
        [JsonProperty("incoterm")]
        public string? Incoterm { get; set; }

        /// <summary>
        ///     Invoice number to print on the postage label.
        /// </summary>
        [JsonProperty("invoice_number")]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        ///     Set the date that will appear on the postage label.
        ///     Accepts ISO 8601 formatted string including time zone offset.
        ///     EasyPost stores all dates as UTC time.
        /// </summary>
        [JsonProperty("label_date")]
        public DateTime? LabelDate { get; set; }

        /// <summary>
        ///     Supported label formats are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"PNG"</description>
        ///         </item>
        ///         <item>
        ///             <description>"PDF"</description>
        ///         </item>
        ///         <item>
        ///             <description>"ZPL"</description>
        ///         </item>
        ///         <item>
        ///             <description>"EPL2"</description>
        ///         </item>
        ///     </list>
        ///     "PNG" is the only format that allows for conversion.
        /// </summary>
        [JsonProperty("label_format")]
        public string? LabelFormat { get; set; }
        [JsonProperty("label_size")]
        public string? LabelSize { get; set; }
        [JsonProperty("license_number")]
        public string? LicenseNumber { get; set; }

        /// <summary>
        ///     Whether or not the parcel can be processed by the carriers equipment.
        /// </summary>
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

        /// <summary>
        ///     Setting payment type to bill the correct account for purchasing postage.
        ///     "THIRD_PARTY" is only supported on passthrough billed carriers.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Key</term>
        ///             <description>Value</description>
        ///         </listheader>
        ///         <item>
        ///             <term>type</term>
        ///             <description>One of the following:
        ///                 <list type="bullet">
        ///                     <item>
        ///                         <description>"SENDER"</description>
        ///                     </item>
        ///                     <item>
        ///                         <description>"THIRD_PARTY"</description>
        ///                     </item>
        ///                     <item>
        ///                         <description>"RECEIVER"</description>
        ///                     </item>
        ///                     <item>
        ///                         <description>"COLLECT"</description>
        ///                     </item>
        ///                 </list>
        ///                 Defaults to "SENDER".
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>account</term>
        ///             <description>Account number. Required for "RECEIVER" and "THIRD_PARTY".</description>
        ///         </item>
        ///         <item>
        ///             <term>country</term>
        ///             <description>Country code that the account is based in. Required for "THIRD_PARTY".</description>
        ///         </item>
        ///         <item>
        ///             <term>postage_code</term>
        ///             <description>Postal code that the account is based in. Required for "RECEIVER" and "THIRD_PARTY".</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("payment")]
        public Dictionary<string, object>? Payment { get; set; }
        [JsonProperty("peel_and_return")]
        public bool? PeelAndReturn { get; set; }
        /// <summary>
        ///     The latest a package should be picked up.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("pickup_max_datetime")]
        public DateTime? PickupMaxDatetime { get; set; }
        /// <summary>
        ///     The earliest a package should be picked up.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("pickup_min_datetime")]
        public DateTime? PickupMinDatetime { get; set; }
        [JsonProperty("po_sort")]
        public string? PoSort { get; set; }
        [JsonProperty("postage_label_inline")]
        public bool? PostageLabelInline { get; set; }
        [JsonProperty("print_custom")]
        public List<Dictionary<string, object>>? PrintCustom { get; set; }
        /// <summary>
        ///     You can optionally print custom messages on labels.
        ///     Message to print on the label in spot 1.
        /// </summary>
        [JsonProperty("print_custom_1")]
        public string? PrintCustom1 { get; set; }

        /// <summary>
        ///     Create a barcode for this custom reference if supported by carrier.
        /// </summary>
        [JsonProperty("print_custom_1_barcode")]
        public bool? PrintCustom1Barcode { get; set; }

        /// <summary>
        ///     Specify the type of <see cref="PrintCustom1"/>.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Carrier</term>
        ///             <description>Details</description>
        ///         </listheader>
        ///         <item>
        ///             <term>FedEx</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>(null)</term>
        ///                         <description>If <see cref="PrintCustom1"/> is not provided, it defaults to Customer Reference</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>PO</term>
        ///                         <description>Purchase Order Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>DP</term>
        ///                         <description>Department Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RMA</term>
        ///                         <description>Return Merchandise Authorization</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>UPS</term>
        ///             <description>
        ///                 <list type="table">
        ///                     <item>
        ///                         <term>AJ</term>
        ///                         <description>Accounts Receivable Customer Account</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>AT</term>
        ///                         <description>Appropriation Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>BM</term>
        ///                         <description>Bill of Lading Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>9V</term>
        ///                         <description>Collect on Delivery (COD) Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ON</term>
        ///                         <description>Dealer Order Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>DP</term>
        ///                         <description>Department Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>3Q</term>
        ///                         <description>Food and Drug Administration (FDA) Product Code</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>IK</term>
        ///                         <description>Invoice Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>MK</term>
        ///                         <description>Manifest Key Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>MJ</term>
        ///                         <description>Model Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>PM</term>
        ///                         <description>Part Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>PC</term>
        ///                         <description>Production Code</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>PO</term>
        ///                         <description>Purchase Order Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RQ</term>
        ///                         <description>Purchase Request Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RZ</term>
        ///                         <description>Return Authorization Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SA</term>
        ///                         <description>Salesperson Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SE</term>
        ///                         <description>Serial Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ST</term>
        ///                         <description>Store Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>TN</term>
        ///                         <description>Transaction Reference Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>EI</term>
        ///                         <description>Employer's ID Number</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>TJ</term>
        ///                         <description>Federal Taxpayer ID No.</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("print_custom_1_code")]
        public string? PrintCustom1Code { get; set; }

        /// <summary>
        ///     You can optionally print custom messages on labels.
        ///     Message to print on the label in spot 2.
        /// </summary>
        [JsonProperty("print_custom_2")]
        public string? PrintCustom2 { get; set; }

        /// <summary>
        ///     Create a barcode for this custom reference if supported by carrier.
        /// </summary>
        [JsonProperty("print_custom_2_barcode")]
        public bool? PrintCustom2Barcode { get; set; }

        /// <summary>
        ///     Specify the type of <see cref="PrintCustom2"/>.
        ///     See <see cref="PrintCustom1Code"/> for a list of valid values.
        /// </summary>
        [JsonProperty("print_custom_2_code")]
        public string? PrintCustom2Code { get; set; }

        /// <summary>
        ///     You can optionally print custom messages on labels.
        ///     Message to print on the label in spot 3.
        /// </summary>
        [JsonProperty("print_custom_3")]
        public string? PrintCustom3 { get; set; }

        /// <summary>
        ///     Create a barcode for this custom reference if supported by carrier.
        /// </summary>
        [JsonProperty("print_custom_3_barcode")]
        public bool? PrintCustom3Barcode { get; set; }

        /// <summary>
        ///     Specify the type of <see cref="PrintCustom3"/>.
        ///     See <see cref="PrintCustom1Code"/> for a list of valid values.
        /// </summary>
        [JsonProperty("print_custom_3_code")]
        public string? PrintCustom3Code { get; set; }
        [JsonProperty("print_rate")]
        public bool? PrintRate { get; set; }
        [JsonProperty("receiver_liquor_license")]
        public string? ReceiverLiquorLicense { get; set; }

        /// <summary>
        ///     Registered Mail is the most secure service that the USPS offers.
        ///     It incorporates a system of receipts to monitor the movement of the mail from the point of acceptance to delivery.
        /// </summary>
        [JsonProperty("registered_mail")]
        public bool? RegisteredMail { get; set; }

        /// <summary>
        ///     The value of the package contents for <see cref="RegisteredMail"/> purposes.
        /// </summary>
        [JsonProperty("registered_mail_amount")]
        public double? RegisteredMailAmount { get; set; }

        /// <summary>
        ///     An electronic return receipt may be purchased at the time of mailing and provides a shipper with evidence of delivery (to whom the mail was delivered and date of delivery), and information about the recipient's actual delivery address.
        ///     Only applies to the USPS.
        /// </summary>
        [JsonProperty("return_receipt")]
        public bool? ReturnReceipt { get; set; }
        [JsonProperty("return_service")]
        public string? ReturnService { get; set; }

        /// <summary>
        ///     Set this value to true for delivery on Saturday.
        ///     When set, you will only get rates for services that are eligible for Saturday delivery.
        ///     If no services are available for Saturday delivery, then you will not be returned any rates.
        ///     You may need to create two shipments, one with the <see cref="SaturdayDelivery"/> option set and one without to get all your eligible rates.
        /// </summary>
        [JsonProperty("saturday_delivery")]
        public bool? SaturdayDelivery { get; set; }
        [JsonProperty("settlement_method")]
        public string? SettlementMethod { get; set; }

        /// <summary>
        ///     You can use this to override the hub ID you have on your account.
        /// </summary>
        [JsonProperty("smartpost_hub")]
        public string? SmartpostHub { get; set; }

        /// <summary>
        ///     The manifest ID is used to group SmartPost packages onto a manifest for each trailer.
        /// </summary>
        [JsonProperty("smartpost_manifest")]
        public string? SmartpostManifest { get; set; }

        /// <summary>
        ///     This option allows you to request the following USPS mail classes:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Type</term>
        ///             <description>Code</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Media Mail</term>
        ///             <description>USPS.MEDIAMAIL</description>
        ///         </item>
        ///         <item>
        ///             <term>Library Mail</term>
        ///             <description>USPS.LIBRARYMAIL</description>
        ///         </item>
        ///     </list>
        ///     These mail classes have restrictions that must be followed.
        ///     See the DMM (https://pe.usps.com/DMM300/Index) for eligibility rules.
        /// </summary>
        [JsonProperty("special_rates_eligibility")]
        public string? SpecialRatesEligibility { get; set; }
        [JsonProperty("suppress_etd")]
        public bool? SuppressEtd { get; set; }

        #endregion

    }
}
