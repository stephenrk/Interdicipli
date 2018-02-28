using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindMVC.Controllers
{
    public class CustomerController : Controller
    {
        private NorthWind db = new NorthWind();

        [HttpGet]
        public ActionResult ListCountries(string id)
        {
            var list = (from c in db.Customers
                        where c.Country != null
                            && (string.IsNullOrEmpty(id) ? true : c.Country.Contains(id))
                        orderby c.Country
                        select c.Country).ToList().Distinct();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string id)
        {
            var customer = (from c in db.Customers where c.CustomerID == id select c).FirstOrDefault();
            if (id == null)
                return View("Erro", new Exception("Registro não Encontrado"));
            return View(customer);
        }

        public ActionResult Index(Customer customer)
        {
            var customers = (from c in db.Customers
                             where (string.IsNullOrEmpty(customer.CompanyName) ? true : c.CompanyName.Contains(customer.CompanyName))
                             && (string.IsNullOrEmpty(customer.Country) ? true : c.Country.Contains(customer.Country))
                              orderby c.CompanyName
                             select c).ToList();
            ViewBag.CompanyName = customer.CompanyName;
            ViewBag.Country = customer.Country;
            return View(customers);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                db.Customers.Attach(customer);
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }
            ViewBag.Message = "O registro foi alterado com sucesso";
            return View("Details", customer);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var customer = (from c in db.Customers where c.CustomerID == id select c).FirstOrDefault();
            if (customer == null)
            {
                var ex = new Exception("resgistro nao encontrado");
                return View("Error", ex);
            }
            return View("New", customer);

        }
        public ActionResult Delete(string ID)
        {
            var customer = (from c in db.Customers orderby c.CompanyName where c.CustomerID == ID select c).FirstOrDefault();
            if (customer == null)
            {
                var ex = new Exception("resgistro nao encontrado");
                return View("Error", ex);
            }
            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return View("Erro", ErrorException(ex));
            }
            ViewBag.Message = "O registro foi deletado";
            return View("Details",customer);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }
            ViewBag.Message = "o registro foi incluido com sucesso!";
            return View("Details", customer);
        }

        private Exception ErrorException(Exception ex)
        {
            if (ex.InnerException == null) { return ex; }
            else { return ErrorException(ex.InnerException); }
        }
    }
}