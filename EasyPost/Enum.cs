using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public enum Carrier {
        FedEx,
        FedExSmartPost,
        UPS,
        UPSMailInnovations,
        UPSSurePost,
        USPS,
    }

    public enum PredefinedPackage {
        Card,
        FedEx10kgBox,
        FedEx25kgBox,
        FedExBox,
        FedExEnvelope,
        FedExPak,
        FedExTube,
        Flat,
        FlatRateCardboardEnvelope,
        FlatRateEnvelope,
        FlatRateGiftCardEnvelope,
        FlatRateLegalEnvelope,
        FlatRatePaddedEnvelope,
        FlatRateWindowEnvelope,
        IrregularParcel,
        LargeExpressBox,
        LargeFlatRateBoardGameBox,
        LargeFlatRateBox,
        LargeParcel,
        Letter,
        MediumExpressBox,
        MediumFlatRateBox,
        Pak,
        Pallet,
        Parcel,
        RegionalRateBoxA,
        RegionalRateBoxB,
        RegionalRateBoxC,
        SmallExpressBox,
        SmallFlatRateBox,
        SmallFlatRateEnvelope,
        Tube,
        UPS10kgBox,
        UPS25kgBox,
        UPSExpressBox,
        UPSLetter,
    }

    public enum Service {
        // USPS
        CriticalMail,
        Express,
        ExpressMailInternational,
        First,
        FirstClassMailInternational,
        FirstClassPackageInternationalService,
        LibertyMail,
        MediaMail,
        ParcelPost,
        ParcelSelect,
        Priority,
        PriorityMailInternational,
        StandardMail,

        // UPS
        // Express shared with USPS
        Expedited,
        ExpressPlus,
        Ground,
        NextDayAir,
        NextDayAirEarlyAM,
        NextDayAirSaver,
        SecondDayAir,
        SecondDayAirAM,
        ThreeDaySelect,
        UPSSaver,
        UPSStandard,
        UPSTodayDedicatedCourier,
        UPSTodayExpress,
        UPSTodayExpressSaver,
        UPSTodayIntercity,
        UPSTodayStandard,

        // UPSMailInnovations
        // First, Priority shared with USPS
        EcononmyMailInnovations,
        ExpeditedMailInnovations,
        PriorityMailInnovations,

        // UPSSurePost
        SurePostOver1Lb,
        SurePostUnder1Lb,

        // FedEx & FedExSmartPost
        EUROPE_FIRST_INTERNATIONAL_PRIORITY,
        FEDEX_1_DAY_FREIGHT,
        FEDEX_2_DAY,
        FEDEX_2_DAY_AM,
        FEDEX_2_DAY_FREIGHT,
        FEDEX_EXPRESS_SAVER,
        FEDEX_FIRST_FREIGHT,
        FEDEX_FREIGHT_ECONOMY,
        FEDEX_FREIGHT_PRIORITY,
        FEDEX_GROUND,
        FIRST_OVERNIGHT,
        GROUND_HOME_DELIVERY,
        INTERNATIONAL_ECONOMY,
        INTERNATIONAL_ECONOMY_FREIGHT,
        INTERNATIONAL_FIRST,
        INTERNATIONAL_PRIORITY,
        INTERNATIONAL_PRIORITY_FREIGHT,
        PRIORITY_OVERNIGHT,
        SMART_POST,
        STANDARD_OVERNIGHT,
    }
}
