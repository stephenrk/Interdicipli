using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindMVC.Controllers
{
    public class CategoryController : Controller
    {
        private System.Exception ErrorException(System.Exception ex)///////////////////////
        {
            if (ex.InnerException == null) { return ex; }
            else { return ErrorException(ex.InnerException); }
        }

        private NorthWind db = new NorthWind();


        public ActionResult IndexJavaScript(Exception ex) ///////////////////////
        {
            return View();
        }


        [HttpGet]
        public ActionResult ListCategories(string id)
        {
            var list = (from c in db.Categories
                        where c.CategoryName != null
                            && (string.IsNullOrEmpty(id) ? true : c.CategoryName.StartsWith(id))
                        orderby c.CategoryName
                        select c
                        ).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}