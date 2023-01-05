#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostService : WithClient, IEasyPostService
    {
#pragma warning disable IDE0021
        internal EasyPostService(EasyPostClient client)
        {
            Client = client;
        }
#pragma warning restore IDE0021
    }

    public interface IEasyPostService
    {
    }
}
