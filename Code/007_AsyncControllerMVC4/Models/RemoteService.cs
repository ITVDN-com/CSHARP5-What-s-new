using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncControllerMVC4.Models
{
    public class RemoteService:IDisposable
    {
        public string GetRemoteData()
        {
            Thread.Sleep(2000);

            return String.Format("Hello from remote service Поток модели:{0}, IsThreadPoolThread: {1}",
                                          Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        public async Task<string> GetRemoteDataAsync()
        {
           return await Task.Run(()=>GetRemoteData());
        }

        public void Dispose()
        {
           //TODO Free used resources
        }
    }
}