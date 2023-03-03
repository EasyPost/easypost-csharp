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
    ///     An interface marking that an instance of the implementing class can be used as a a shipment parameter in a Parameters object.
    /// </summary>
    public interface IShipmentParameter : IParameter
    {
    }
}
