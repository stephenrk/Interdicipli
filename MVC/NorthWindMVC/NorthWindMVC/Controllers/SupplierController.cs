using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindMVC.Controllers
{
    public class SupplierController : Controller
    {
        private NorthWind db = new NorthWind();///////////////////////


        [HttpGet]
        public ActionResult ListSuppliers(string id)
        {
            var list = (from s in db.Suppliers
                        where s.CompanyName != null
                            && (string.IsNullOrEmpty(id) ? true : s.CompanyName.StartsWith(id))
                        orderby s.CompanyName
                        select new {
                            s.CompanyName, s.SupplierID
                        }
                        ).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        private System.Exception ErrorException(System.Exception ex)///////////////////////
        {
            if (ex.InnerException == null) { return ex; }
            else { return ErrorException(ex.InnerException); }
        }
        

        public ActionResult NewJavaScript(int? id) ///////////////////////
        {
            Supplier Supplier = new Supplier();
            if (id != null)
            {
                Supplier = (from e in db.Suppliers
                                where e.SupplierID == id
                                select e).FirstOrDefault();
            }
            return View(Supplier);
        }
       
        [HttpPost]
        public ActionResult SaveJson(Supplier e)
        {
            var resposta = new RespostaHtml { success = true };

            try
            {                
                if (e.SupplierID == 0)
                {
                    db.Suppliers.Add(e);
                }
                else
                {
                    db.Suppliers.Attach(e);
                    db.Entry(e).State = EntityState.Modified;
                }                
                db.SaveChanges();
                resposta.Data = e;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                resposta.success = false;
                resposta.message = ex.Message;
            }
            return Json(resposta, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult DeleteJson(Supplier e)
        {
            var resposta = new RespostaHtml { success = true };

            var Supplier = (from emp in db.Suppliers
                            where emp.SupplierID == e.SupplierID
                            select emp).FirstOrDefault();
            if (Supplier == null)
            {
                resposta.success = false;
                resposta.message = "Registro não Encontrado";
                return Json(resposta, JsonRequestBehavior.DenyGet);
            }
            try
            {
                db.Suppliers.Remove(Supplier);
                db.SaveChanges();
                resposta.Data = Supplier;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                resposta.success = false;
                resposta.message = ex.Message;
            }

            return Json(resposta, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int SupplierID)
        {
            var Supplier = (from emp in db.Suppliers
                            where emp.SupplierID == SupplierID
                            select emp).FirstOrDefault();
            if (Supplier == null)
            {
                var ex = new Exception("O registro nao foi encontrado!");
                return View("Error", ex);
            }
            try
            {
                db.Suppliers.Remove(Supplier);
                db.SaveChanges();

                var emps = (from emp in db.Suppliers select emp).ToList();
                ViewBag.Empregados = emps;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }

            ViewBag.Message = "O registro foi Removido!";
            return View("New", new Supplier());
        }

        public ActionResult IndexJavaScript(Exception ex) ///////////////////////
        {
            return View();
        }
        public ActionResult ListSuppliersJson(string CompanyName) ///////////////////////
        {
            var funcionarios = (from e in db.Suppliers
                                where (string.IsNullOrEmpty(CompanyName)
                                ? true : e.CompanyName.StartsWith(CompanyName))
                                orderby e.CompanyName
                                select e).ToList();
            return Json( funcionarios,JsonRequestBehavior.AllowGet);
        }
    }
}