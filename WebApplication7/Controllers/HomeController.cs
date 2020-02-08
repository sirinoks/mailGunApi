using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication7.Controllers
{
    //not used
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Send()
        {
            ViewBag.Message = "Send an email";

            return View();
        }

        public ActionResult Logs()
        {
            ViewBag.Message = "Emails for today";

            return View();
        }
    }
}