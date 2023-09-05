using System.Collections.Generic;
using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API;

public class CarrierAccountType : ValueEnum
{
    public static readonly CarrierAccountType FedEx = new CarrierAccountType(26, "FedexAccount");
    public static readonly CarrierAccountType FedExSmartPost = new CarrierAccountType(30, "FedexSmartpostAccount");
    public static readonly CarrierAccountType Ups = new CarrierAccountType(59, "UpsAccount");

    private CarrierAccountType(int id, string name)
        : base(id, name)
    {
    }

    public string Name => (string)Value;

    public static IEnumerable<CarrierAccountType> All() => GetAll<CarrierAccountType>();
}
