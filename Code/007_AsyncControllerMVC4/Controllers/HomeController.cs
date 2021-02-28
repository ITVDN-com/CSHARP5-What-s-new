using System.Web.Mvc;
using AsyncControllerMVC4.Models;

namespace AsyncControllerMVC4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var service = new RemoteService();

            ViewBag.Message = service.GetRemoteData();

            return View();
        }
    }
}
