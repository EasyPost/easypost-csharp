namespace EasyPost.BetaFeatures.Parameters
{
    /// <summary>
    ///     The base interface for all objects that can be used as a parameter in Parameters.
    /// </summary>
    public interface IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a an address parameter in a Parameters object.
    /// </summary>
    public interface IAddressParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a a batch parameter in a Parameters object.
    /// </summary>
    public interface IBatchParameter : IParameter
    {
    }

    public interface ICarrierAccountParameter : IParameter
    {
    }

    public interface ICustomsInfoParameter : IParameter
    {
    }

    public interface ICustomsItemParameter : IParameter
    {
    }

    public interface IEndShipperParameter : IParameter
    {
    }

    public interface IInsuranceParameter : IParameter
    {
    }

    public interface IOrderParameter : IParameter
    {
    }

    public interface IParcelParameter : IParameter
    {
    }

    public interface IPickupParameter : IParameter
    {
    }

    public interface IReferralCustomerParameter : IParameter
    {
    }

    public interface IRefundParameter : IParameter
    {
    }

    public interface IReportParameter : IParameter
    {
    }

    public interface IScanFormParameter : IParameter
    {
    }

    /// <summary>
    ///     An interface marking that an instance of the implementing class can be used as a a shipment parameter in a Parameters object.
    /// </summary>
    public interface IShipmentParameter : IParameter
    {
    }

    public interface ITrackerParameter : IParameter
    {
    }

    public interface IUserParameter : IParameter
    {
    }

    public interface IWebhookParameter : IParameter
    {
    }
}
