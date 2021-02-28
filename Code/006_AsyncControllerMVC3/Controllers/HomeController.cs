using System.Web.Mvc;
using _04_AsyncController.Models;

namespace _04_AsyncController.Controllers
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
            using (var service = new RemoteService())
            {
                ViewBag.Message = service.GetRemoteData();
            }
            return View();
        }
    }
}
