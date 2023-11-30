using System;
using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Utilities.Internal.Extensions;
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

        [JsonProperty("administratively_unpurchasable")]
        public bool? AdministrativelyUnpurchasable { get; set; }

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

        [JsonProperty("auto_manifest")]
        public bool? AutoManifest { get; set; }

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

        [JsonProperty("carrier_branded")]
        public bool? CarrierBranded { get; set; }

        [JsonProperty("carrier_instructions")]
        public Dictionary<string, object?>? CarrierInstructions { get; set; } // hash

        [JsonProperty("carrier_insurance_amount")]
        public string? CarrierInsuranceAmount { get; set; } // number

        [JsonProperty("carrier_notification_email")]
        public string? CarrierNotificationEmail { get; set; }

        [JsonProperty("carrier_notification_sms")]
        public string? CarrierNotificationSms { get; set; }

        [JsonProperty("certificate_number")]
        public string? CertificateNumber { get; set; } // any

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
        public string? CodAddressId { get; set; } // any

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
        public string? CommercialInvoiceLetterhead { get; set; } // any

        [JsonProperty("commercial_invoice_signature")]
        public string? CommercialInvoiceSignature { get; set; } // any

        [JsonProperty("commercial_invoice_size")]
        public string? CommercialInvoiceSize { get; set; }

        [JsonProperty("container")]
        public Dictionary<string, object?>? Container { get; set; } // hash

        /// <summary>
        ///     A description of the content of the shipment.
        /// </summary>
        [JsonProperty("content_description")]
        public string? ContentDescription { get; set; }

        [JsonProperty("cost_center")]
        public string? CostCenter { get; set; } // any

        /// <summary>
        ///     Which currency this <see cref="Shipment"/> will show for rates if carrier allows.
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("customs_broker_address_id")]
        public string? CustomsBrokerAddressId { get; set; } // any

        [JsonProperty("customs_include_shipping")]
        public bool? CustomsIncludeShipping { get; set; } // bool

        [JsonProperty("customs_unit_of_measurement")]
        public string? CustomsUnitOfMeasurement { get; set; }

        [JsonProperty("dangerous_goods")]
        public Dictionary<string, object>? DangerousGoods { get; set; } // hash

        [JsonProperty("declared_value")]
        public double? DeclaredValue { get; set; } // number

        [JsonProperty("delivered_duty_paid")]
        public bool? DeliveredDutyPaid { get; set; } // bool

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
        public string? DeliveryConfirmation { get; set; } // any

        [JsonProperty("delivery_date")]
        public string? DeliveryDate { get; set; }

        /// <summary>
        ///     The latest a package should be delivered.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("delivery_max_datetime")]
        public DateTime? DeliveryMaxDatetime { get; set; } // datetime

        /// <summary>
        ///     The earliest a package should be delivered.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("delivery_min_datetime")]
        public DateTime? DeliveryMinDatetime { get; set; } // datetime

        [JsonProperty("delivery_time_preference")]
        public string? DeliveryTimePreference { get; set; }

        [JsonProperty("dhlgm_return_location")]
        public string? DhlgmReturnLocation { get; set; }

        [JsonProperty("discrete_postage_labels")]
        public bool? DiscretePostageLabels { get; set; } // bool

        [JsonProperty("dropoff_max_datetime")]
        public DateTime? DropoffMaxDatetime { get; set; }

        [JsonProperty("dropoff_min_datetime")]
        public DateTime? DropoffMinDatetime { get; set; }

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
        public string? DropoffType { get; set; }  // enum server-side

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
        public bool? DryIce { get; set; } // bool

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
        ///             <description>
        ///                 One of the following:
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
        public Dictionary<string, object?>? DutyPayment { get; set; } // hash

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
        public string? Endorsement { get; set; } // enum server-side

        /// <summary>
        ///     Specify the responsible <see cref="EndShipper"/> for the shipment.
        /// </summary>
        [JsonProperty("end_shipper_id")]
        public string? EndShipperId { get; set; }

        [JsonProperty("entry")]
        public string? Entry { get; set; } // any

        [JsonProperty("facility_code")]
        public string? FacilityCode { get; set; }

        [JsonProperty("fims_awb_number")]
        public string? FimsAwbNumber { get; set; } // any

        /// <summary>
        ///     Additional cost to be added to the invoice of this <see cref="Shipment"/>.
        ///     Only applies to UPS currently.
        /// </summary>
        [JsonProperty("freight_charge")]
        public string? FreightCharge { get; set; } // number

        [JsonProperty("fulfiller_order_id")]
        public string? FulfillerOrderId { get; set; }

        [JsonProperty("fulfiller_order_items")]
        public List<string>? FulfillerOrderItems { get; set; } // array

        [JsonProperty("group")]
        public string? Group { get; set; } // any

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
        public string? Hazmat { get; set; } // enum server-side

        /// <summary>
        ///     Package will wait at carrier facility for pickup.
        /// </summary>
        [JsonProperty("hold_for_pickup")]
        public bool? HoldForPickup { get; set; } // bool

        [JsonProperty("image_format")]
        public string? ImageFormat { get; set; }

        [JsonProperty("import_control")]
        public string? ImportControl { get; set; } // enum server-side

        [JsonProperty("import_control_description")]
        public string? ImportControlDescription { get; set; } // any

        [JsonProperty("importer_address_id")]
        public string? ImporterAddressId { get; set; } // any

        [JsonProperty("import_federal_tax_id")]
        public string? ImportFederalTaxId { get; set; } // any

        [JsonProperty("import_state_tax_id")]
        public string? ImportStateTaxId { get; set; } // any

        [JsonProperty("importer_id")]
        public Dictionary<string, object?>? ImporterId { get; set; } // hash

        [JsonProperty("importer_reference")]
        public string? ImporterReference { get; set; } // any

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
        public string? Incoterm { get; set; } // enum server-side

        /// <summary>
        ///     Invoice number to print on the postage label.
        /// </summary>
        [JsonProperty("invoice_number")]
        public string? InvoiceNumber { get; set; } // any

        /// <summary>
        ///     Set the date that will appear on the postage label.
        ///     Accepts ISO 8601 formatted string including time zone offset.
        ///     EasyPost stores all dates as UTC time.
        /// </summary>
        [JsonProperty("label_date")]
        public DateTime? LabelDate { get; set; }

        [JsonProperty("label_extension")]
        public Dictionary<string, object?>? LabelExtension { get; set; } // hash

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
        public string? LicenseNumber { get; set; } // any

        [JsonProperty("live_animal")]
        public bool? LiveAnimal { get; set; } // todo: type

        /// <summary>
        ///     Whether or not the parcel can be processed by the carriers equipment.
        /// </summary>
        [JsonProperty("machinable")]
        public bool? Machinable { get; set; } // bool

        [JsonProperty("merchant_id")]
        public string? MerchantId { get; set; } // any

        [JsonProperty("movement_type")]
        public string? MovementType { get; set; } // bool

        [JsonProperty("nafta_certificate_of_origin")]
        public bool? NaftaCertificateOfOrigin { get; set; } // bool

        [JsonProperty("neutral_delivery")]
        public bool? NeutralDelivery { get; set; } // bool

        [JsonProperty("non_contact")]
        public bool? NonContact { get; set; } // bool

        [JsonProperty("notifications")]
        public List<string>? Notifications { get; set; } // array

        [JsonProperty("one_page")]
        public bool? OnePage { get; set; }

        [JsonProperty("origin_terminal")]
        public string? OriginTerminal { get; set; } // any

        [JsonProperty("overlabel_construct_code")]
        public string? OverlabelConstructCode { get; set; }

        [JsonProperty("overlabel_construct_tracking_number")]
        public string? OverlabelOriginalTrackingNumber { get; set; }

        [JsonProperty("partial_delivery")]
        public bool? PartialDelivery { get; set; } // todo: type

        [JsonProperty("parties_to_transaction_are_related")]
        public bool? PartiesToTransactionAreRelated { get; set; } // bool

        [JsonProperty("passport_issue_date")]
        public string? PassportIssueDate { get; set; }

        [JsonProperty("passport_issued_by")]
        public string? PassportIssuedBy { get; set; }

        [JsonProperty("passport_number")]
        public string? PassportNumber { get; set; }

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
        ///             <description>
        ///                 One of the following:
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
        public Dictionary<string, object?>? Payment { get; set; } // hash

        [JsonProperty("peel_and_return")]
        public bool? PeelAndReturn { get; set; } // bool

        [JsonProperty("perishable")]
        public bool? Perishable { get; set; } // bool

        [JsonProperty("pharmacy")]
        public bool? Pharmacy { get; set; } // bool

        /// <summary>
        ///     The latest a package should be picked up.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("pickup_max_datetime")]
        public DateTime? PickupMaxDatetime { get; set; } // datetime

        /// <summary>
        ///     The earliest a package should be picked up.
        ///     Supported carriers vary.
        /// </summary>
        [JsonProperty("pickup_min_datetime")]
        public DateTime? PickupMinDatetime { get; set; } // datetime

        [JsonProperty("po_sort")]
        public string? PoSort { get; set; }

        [JsonProperty("postage_label_inline")]
        public bool? PostageLabelInline { get; set; } // bool

        [JsonProperty("print_custom")]
        public List<string>? PrintCustom { get; set; } // array

        /// <summary>
        ///     You can optionally print custom messages on labels.
        ///     Message to print on the label in spot 1.
        /// </summary>
        [JsonProperty("print_custom_1")]
        public string? PrintCustom1 { get; set; } // any

        /// <summary>
        ///     Create a barcode for this custom reference if supported by carrier.
        /// </summary>
        [JsonProperty("print_custom_1_barcode")]
        public bool? PrintCustom1Barcode { get; set; } // bool

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
        public string? PrintCustom2 { get; set; } // any

        /// <summary>
        ///     Create a barcode for this custom reference if supported by carrier.
        /// </summary>
        [JsonProperty("print_custom_2_barcode")]
        public bool? PrintCustom2Barcode { get; set; } // bool

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
        public string? PrintCustom3 { get; set; } // any

        /// <summary>
        ///     Create a barcode for this custom reference if supported by carrier.
        /// </summary>
        [JsonProperty("print_custom_3_barcode")]
        public bool? PrintCustom3Barcode { get; set; } // bool

        /// <summary>
        ///     Specify the type of <see cref="PrintCustom3"/>.
        ///     See <see cref="PrintCustom1Code"/> for a list of valid values.
        /// </summary>
        [JsonProperty("print_custom_3_code")]
        public string? PrintCustom3Code { get; set; }

        [JsonProperty("print_rate")]
        public bool? PrintRate { get; set; } // bool

        [JsonProperty("priority_alert")]
        public bool? PriorityAlert { get; set; } // bool

        [JsonProperty("priority_alert_content")]
        public string? PriorityAlertContent { get; set; } // any

        [JsonProperty("priority_alert_plus")]
        public bool? PriorityAlertPlus { get; set; } // bool

        [JsonProperty("receiver_liquor_license")]
        public bool? ReceiverLiquorLicense { get; set; } // bool

        /// <summary>
        ///     Registered Mail is the most secure service that the USPS offers.
        ///     It incorporates a system of receipts to monitor the movement of the mail from the point of acceptance to delivery.
        /// </summary>
        [JsonProperty("registered_mail")]
        public bool? RegisteredMail { get; set; } // bool

        /// <summary>
        ///     The value of the package contents for <see cref="RegisteredMail"/> purposes.
        /// </summary>
        [JsonProperty("registered_mail_amount")]
        public double? RegisteredMailAmount { get; set; } // number

        /// <summary>
        ///     An electronic return receipt may be purchased at the time of mailing and provides a shipper with evidence of delivery (to whom the mail was delivered and date of delivery), and information about the recipient's actual delivery address.
        ///     Only applies to the USPS.
        /// </summary>
        [JsonProperty("return_receipt")]
        public bool? ReturnReceipt { get; set; } // bool

        [JsonProperty("return_service")]
        public string? ReturnService { get; set; } // any

        /// <summary>
        ///     Set this value to true for delivery on Saturday.
        ///     When set, you will only get rates for services that are eligible for Saturday delivery.
        ///     If no services are available for Saturday delivery, then you will not be returned any rates.
        ///     You may need to create two shipments, one with the <see cref="SaturdayDelivery"/> option set and one without to get all your eligible rates.
        /// </summary>
        [JsonProperty("saturday_delivery")]
        public bool? SaturdayDelivery { get; set; } // bool

        [JsonProperty("service_codes")]
        public List<string>? ServiceCodes { get; set; } // array

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
        public string? SmartpostManifest { get; set; } // any

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

        [JsonProperty("sub_shipper_id")]
        public string? SubShipperId { get; set; } // any

        [JsonProperty("suppress_etd")]
        public bool? SuppressEtd { get; set; } // bool

        /// <summary>
        ///     The expiration date of the tax ID, in the format DD/MM/YYYY.
        /// </summary>
        [JsonProperty("tax_id_expiration_date")]
        public string? TaxIdExpirationDate { get; set; }

        [JsonProperty("third_party_consignee")]
        public bool? ThirdPartyConsignee { get; set; } // bool

        [JsonProperty("ups_return_service")]
        public string? UpsReturnService { get; set; }

        #endregion

        private readonly Dictionary<string, object> _additionalOptions = new();

        /// <summary>
        ///     Add an additional option that is not officially supported by the library.
        /// </summary>
        /// <param name="key">JSON key of the option to add.</param>
        /// <param name="value">Value of the option to add.</param>
        public void AddAdditionalOption(string key, object value) => _additionalOptions.Add(key, value);

        public override Dictionary<string, object> AsDictionary()
        {
            Dictionary<string, object> data = base.AsDictionary();

            // Add any additional options
            data = data.MergeIn(_additionalOptions);

            return data;
        }
    }
}
