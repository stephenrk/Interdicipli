using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWindMVC.Models;
using DataNorthwind;
using BusinessNorthwind;

namespace NorthWindMVC.Controllers
{
    public class OrderController : Controller
    {
        private NorthWind db = new NorthWind();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var employees = (from e in db.Employees
                             select e).ToList();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpPost]
        public ActionResult SalvarDados(Order order)
        {
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return Json(order,
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ErrorException(ex),
                    JsonRequestBehavior.AllowGet);
            }
        }

        public System.Exception ErrorException(System.Exception ex)
        {
            if (ex.InnerException == null)
                return ex;
            else
                return ErrorException(ex.InnerException);
        }
        
    }
}