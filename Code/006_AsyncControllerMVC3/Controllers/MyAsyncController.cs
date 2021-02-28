using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using _04_AsyncController.Models;
using System.Threading;

namespace _04_AsyncController.Controllers
{
    public class MyAsyncController : AsyncController
    {
        public void GetDataAsync()
        {
            this.AsyncManager.OutstandingOperations.Increment();
            ViewBag.Message += ".......Начало работы контроллера в потоке: #" + Thread.CurrentThread.ManagedThreadId;
            Task.Factory.StartNew(() =>
            {
                ViewBag.Message+="Вызов удаленной службы был в потоке: #" + Thread.CurrentThread.ManagedThreadId;

                using (var service = new RemoteService())
                {
                    this.AsyncManager.Parameters["data"] = service.GetRemoteData();
                    this.AsyncManager.OutstandingOperations.Decrement();
                }
            });
        }

        public ActionResult GetDataCompleted(string data)
        {
            ViewBag.Message+=".......Обработка ответа от службы в потоке: #" + Thread.CurrentThread.ManagedThreadId;
            ViewBag.Message += "....."+data;
            return View();
        }



    }
}
