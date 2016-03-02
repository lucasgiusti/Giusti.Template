using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Giusti.Template.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }

    public static class HtmlHelperExtensions
    {
        public static bool IsDebug(this HtmlHelper helper)
        {
#if DEBUG
            return true;
#else
          return false;
#endif
        }
    }
}
