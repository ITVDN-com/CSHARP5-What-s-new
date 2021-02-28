using System.Threading;
using System.Web.Mvc;
using System.Threading.Tasks;
using AsyncControllerMVC4.Models;

namespace AsyncControllerMVC4.Controllers
{
    public class MyAsyncController : Controller
    {
        public async Task<ActionResult> GetData()
        {
            var service = new RemoteService();
            ViewBag.Message = await service.GetRemoteDataAsync();
            return View("GetData");
        }
    }
}
