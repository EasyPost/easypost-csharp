using System.Collections.Generic;
using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API;

public class CarrierAccountType : ValueEnum
{
    public static readonly CarrierAccountType AmazonMws = new CarrierAccountType(1, "AmazonMwsAccount");
    public static readonly CarrierAccountType Apc = new CarrierAccountType(2, "ApcAccount");
    public static readonly CarrierAccountType Asendia = new CarrierAccountType(3, "AsendiaAccount");
    public static readonly CarrierAccountType AsendiaUsa = new CarrierAccountType(4, "AsendiaUsaAccount");
    public static readonly CarrierAccountType AustraliaPost = new CarrierAccountType(5, "AustraliaPostAccount");
    public static readonly CarrierAccountType AxlehireV3 = new CarrierAccountType(6, "AxlehireV3Account");
    public static readonly CarrierAccountType BetterTrucks = new CarrierAccountType(7, "BetterTrucksAccount");
    public static readonly CarrierAccountType Bond = new CarrierAccountType(8, "BondAccount");
    public static readonly CarrierAccountType CanadaPost = new CarrierAccountType(9, "CanadaPostAccount");
    public static readonly CarrierAccountType Canpar = new CarrierAccountType(10, "CanparAccount");
    public static readonly CarrierAccountType ColumbusLastMile = new CarrierAccountType(11, "ColumbusLastMileAccount");
    public static readonly CarrierAccountType CourierExpress = new CarrierAccountType(12, "CourierExpressAccount");
    public static readonly CarrierAccountType CouriersPlease = new CarrierAccountType(13, "CouriersPleaseAccount");
    public static readonly CarrierAccountType DaiPost = new CarrierAccountType(14, "DaiPostAccount");
    public static readonly CarrierAccountType DeliverIt = new CarrierAccountType(15, "DeliverItAccount");
    public static readonly CarrierAccountType DeutschePostUk = new CarrierAccountType(16, "DeutschePostUKAccount");
    public static readonly CarrierAccountType DhlEcommerceAsia = new CarrierAccountType(17, "DhlEcommerceAsiaAccount");
    public static readonly CarrierAccountType DhlEcs = new CarrierAccountType(18, "DhlEcsAccount");
    public static readonly CarrierAccountType DhlExpress = new CarrierAccountType(19, "DhlExpressAccount");
    public static readonly CarrierAccountType DhlPaket = new CarrierAccountType(20, "DhlPaketAccount");
    public static readonly CarrierAccountType DhlParcel = new CarrierAccountType(21, "DhlParcelAccount");
    public static readonly CarrierAccountType Dpd = new CarrierAccountType(22, "DpdAccount");
    public static readonly CarrierAccountType DpdUk = new CarrierAccountType(23, "DpdUkAccount");
    public static readonly CarrierAccountType Estafeta = new CarrierAccountType(24, "EstafetaAccount");
    public static readonly CarrierAccountType Fastway = new CarrierAccountType(25, "FastwayAccount");
    public static readonly CarrierAccountType FedEx = new CarrierAccountType(26, "FedexAccount");
    public static readonly CarrierAccountType FedExCrossBorder = new CarrierAccountType(27, "FedexCrossBorderAccount");
    public static readonly CarrierAccountType FedExMailview = new CarrierAccountType(28, "FedexMailviewAccount");
    public static readonly CarrierAccountType FedExSameDayCity = new CarrierAccountType(29, "FedexSamedayCityAccount");
    public static readonly CarrierAccountType FedExSmartPost = new CarrierAccountType(30, "FedexSmartpostAccount");
    public static readonly CarrierAccountType FirstMileConcise = new CarrierAccountType(31, "FirstMileConciseAccount");
    public static readonly CarrierAccountType Globegistics = new CarrierAccountType(32, "GlobegisticsAccount");
    public static readonly CarrierAccountType Gso = new CarrierAccountType(33, "GsoAccount");
    public static readonly CarrierAccountType Hermes = new CarrierAccountType(34, "HermesAccount");
    public static readonly CarrierAccountType InterlinkExpress = new CarrierAccountType(35, "InterlinkExpressAccount");
    public static readonly CarrierAccountType LasershipV2 = new CarrierAccountType(36, "LasershipV2Account");
    public static readonly CarrierAccountType LoomisExpress = new CarrierAccountType(37, "LoomisExpressAccount");
    public static readonly CarrierAccountType Lso = new CarrierAccountType(38, "LsoAccount");
    public static readonly CarrierAccountType Newgistics = new CarrierAccountType(39, "NewgisticsAccount");
    public static readonly CarrierAccountType OmniParcel = new CarrierAccountType(40, "OmniParcelAccount");
    public static readonly CarrierAccountType Ontrac = new CarrierAccountType(41, "OntracAccount");
    public static readonly CarrierAccountType Optima = new CarrierAccountType(42, "OptimaAccount");
    public static readonly CarrierAccountType OsmWorldwide = new CarrierAccountType(43, "OsmWorldwideAccount");
    public static readonly CarrierAccountType ParcelForce = new CarrierAccountType(44, "ParcelForceAccount");
    public static readonly CarrierAccountType Parcll = new CarrierAccountType(45, "ParcllAccount");
    public static readonly CarrierAccountType PassportGlobal = new CarrierAccountType(46, "PassportGlobalAccount");
    public static readonly CarrierAccountType Purolator = new CarrierAccountType(47, "PurolatorAccount");
    public static readonly CarrierAccountType RoyalMail = new CarrierAccountType(48, "RoyalMailAccount");
    public static readonly CarrierAccountType RRDonnelley = new CarrierAccountType(49, "RRDonnelleyAccount");
    public static readonly CarrierAccountType Sendle = new CarrierAccountType(50, "SendleAccount");
    public static readonly CarrierAccountType SFExpress = new CarrierAccountType(51, "SfExpressAccount");
    public static readonly CarrierAccountType SmartKargo = new CarrierAccountType(52, "SmartKargoAccount");
    public static readonly CarrierAccountType Sonic = new CarrierAccountType(53, "SonicAccount");
    public static readonly CarrierAccountType Speedee = new CarrierAccountType(54, "SpeedeeAccount");
    public static readonly CarrierAccountType StarTrack = new CarrierAccountType(55, "StarTrackAccount");
    public static readonly CarrierAccountType Swyft = new CarrierAccountType(56, "SwyftAccount");
    public static readonly CarrierAccountType TForceConcise = new CarrierAccountType(57, "TforceConciseAccount");
    public static readonly CarrierAccountType Uds = new CarrierAccountType(58, "UdsAccount");
    public static readonly CarrierAccountType Ups = new CarrierAccountType(59, "UpsAccount");
    public static readonly CarrierAccountType UpsIparcel = new CarrierAccountType(60, "UpsIparcelAccount");
    public static readonly CarrierAccountType UpsMailInnovations = new CarrierAccountType(61, "UpsMailInnovationsAccount");
    public static readonly CarrierAccountType UpsSurepost = new CarrierAccountType(62, "UpsSurepostAccount");
    public static readonly CarrierAccountType Usps = new CarrierAccountType(63, "UspsAccount");
    public static readonly CarrierAccountType Veho = new CarrierAccountType(64, "VehoAccount");
    public static readonly CarrierAccountType XDelivery = new CarrierAccountType(65, "XDeliveryAccount");

    private CarrierAccountType(int id, string name) : base(id, name)
    {
    }

    public string Name => (string)Value;

    public static IEnumerable<CarrierAccountType> All() => GetAll<CarrierAccountType>();
}
