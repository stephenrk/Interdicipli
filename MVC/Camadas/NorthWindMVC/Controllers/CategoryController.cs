using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataNorthwind;
using BusinessNorthwind;


namespace NorthWindMVC.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCategories()
        {
            var target = new BusinessCategory();
            var retorno = target.GetAllCategories();
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}