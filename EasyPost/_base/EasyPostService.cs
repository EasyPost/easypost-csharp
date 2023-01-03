namespace EasyPost._base
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
