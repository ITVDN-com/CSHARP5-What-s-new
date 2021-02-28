using System;
using System.Threading;

namespace _04_AsyncController.Models
{
    public class RemoteService:IDisposable
    {
        public string GetRemoteData()
        {
            Thread.Sleep(5000);
            return "Hello from remote service";
        }

        public void Dispose()
        {
            //TODO Free resources
        }
    }
}