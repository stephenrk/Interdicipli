using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessNorthwind;
using DataNorthwind;

namespace NorthWindMVC.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public ActionResult ListCountries(string id)
        {
            var busCustomer = new BusinessCustomer();
            var retorno = busCustomer.ListCountries(id);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            var busCustomer = new BusinessCustomer();
            try
            {
                busCustomer.Save(customer);
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }
            ViewBag.Message = "O Registro Foi Alterado com Sucesso!";
            return View("Details", customer);

        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var busCustomer = new BusinessCustomer();

            var customer = busCustomer.GetById(ID);

            if (customer == null)
            {
                var ex = new Exception("Registro Não encontrado!");
                return View("Erro", ex);
            }

            return View("New", customer);

        }

        public ActionResult Delete(string ID)
        {
            var busCustomer = new BusinessCustomer();
            var customer = busCustomer.GetById(ID);
            if (customer == null)
            {
                var ex = new Exception("Registro Não encontrado!");
                return View("Erro", ex);
            }
            try
            {
                busCustomer.Delete(ID);
            }
            catch (Exception ex)
            {
                return View("Erro", ErrorException(ex));
            }

            ViewBag.Message = "O Registro foi excluido com sucesso";
            return View("Details", customer);
        }

        public System.Exception ErrorException(System.Exception ex)
        {
            if (ex.InnerException == null)
                return ex;
            else
                return ErrorException(ex.InnerException);
        }
        public ActionResult Details(string ID)
        {
            var busCustomer = new BusinessCustomer();
            var customer = busCustomer.GetById(ID);

            if (customer == null)
                return View("Erro", new
                    Exception("Registro Não Encontrado"));

            return View(customer);
        }
        public ActionResult Index(Customer customer)
        {
            var busCustomer = new BusinessCustomer();
            var customers = busCustomer.GetAllCustomers(customer);
            ViewBag.CompanyName = customer.CompanyName;
            ViewBag.Country = customer.Country;

            return View(customers);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Customer customer)
        {
            var busCustomer = new BusinessCustomer();
            try
            {
                busCustomer.Save(customer);
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }

            ViewBag.Message = "O Registro Foi Incluido com Sucesso!";
            return View("Details", customer);
        }
    }
}