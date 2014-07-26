using System.Configuration;
using System.Web.Mvc;
using PerDevEnvSettingsDemo.Web.Models;

namespace PerDevEnvSettingsDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var model = new IndexModel
            {
                MyConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString,
                MyAppSettingsValue = ConfigurationManager.AppSettings["MyAppSettingsValue"]
            };
            return View(model);
        }
	}
}