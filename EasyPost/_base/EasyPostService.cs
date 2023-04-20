using System;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostService : WithClient, IEasyPostService, IDisposable
    {
#pragma warning disable IDE0021
        internal EasyPostService(EasyPostClient client)
        {
            Client = client;
        }
#pragma warning restore IDE0021
        public void Dispose()
        {
            Client?.Dispose();

            GC.SuppressFinalize(this);
        }
    }

    public interface IEasyPostService
    {
    }
}
