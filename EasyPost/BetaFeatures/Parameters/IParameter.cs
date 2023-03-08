namespace EasyPost.BetaFeatures.Parameters
{
    /// <summary>
    ///     The base interface for all objects that can be used as a parameter in Parameters.
    /// </summary>
    public interface IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as an address parameter in a Parameters object.
    /// </summary>
    public interface IAddressParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a batch parameter in a Parameters object.
    /// </summary>
    public interface IBatchParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a carrier account parameter in a Parameters object.
    /// </summary>
    public interface ICarrierAccountParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a customs info parameter in a Parameters object.
    /// </summary>
    public interface ICustomsInfoParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a customs item parameter in a Parameters object.
    /// </summary>
    public interface ICustomsItemParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as an end shipper parameter in a Parameters object.
    /// </summary>
    public interface IEndShipperParameter : IAddressParameter // EndShipper object can be used as an "address" parameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as an insurance parameter in a Parameters object.
    /// </summary>
    public interface IInsuranceParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as an order parameter in a Parameters object.
    /// </summary>
    public interface IOrderParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a parcel parameter in a Parameters object.
    /// </summary>
    public interface IParcelParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a pickup parameter in a Parameters object.
    /// </summary>
    public interface IPickupParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a referral customer parameter in a Parameters object.
    /// </summary>
    public interface IReferralCustomerParameter : IParameter
    {
    }

    /// <summary>
    ///    An interface marking that an instance of the implementing class can be used as a refund parameter in a Parameters object.
    /// </summary>
    public interface IRefundParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a report parameter in a Parameters object.
    /// </summary>
    public interface IReportParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a scan form parameter in a Parameters object.
    /// </summary>
    public interface IScanFormParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a shipment parameter in a Parameters object.
    /// </summary>
    public interface IShipmentParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a tax identifier parameter in a Parameters object.
    /// </summary>
    public interface ITaxIdentifierParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a tracker parameter in a Parameters object.
    /// </summary>
    public interface ITrackerParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a user parameter in a Parameters object.
    /// </summary>
    public interface IUserParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a webhook parameter in a Parameters object.
    /// </summary>
    public interface IWebhookParameter : IParameter
    {
    }
}
