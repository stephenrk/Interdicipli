using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataNorthwind;
using BusinessNorthwind;

namespace NorthWindMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Tabela()
        {
            return View();
        }
        public ActionResult OlaMundo()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}