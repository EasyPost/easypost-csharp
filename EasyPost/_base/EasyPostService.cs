namespace EasyPost._base
{
    public abstract class EasyPostService : WithClient, IEasyPostService
    {
        internal EasyPostService(EasyPostClient client)
        {
            Client = client;
        }
    }

    public interface IEasyPostService
    {
    }
}
