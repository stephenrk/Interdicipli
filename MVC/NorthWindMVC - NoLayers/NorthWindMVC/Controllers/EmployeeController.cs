using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private NorthWind db = new NorthWind();///////////////////////


        [HttpGet]
        public ActionResult ListEmployees(string id)
        {
            var list = (from e in db.Employees
                        where e.LastName != null
                            && (string.IsNullOrEmpty(id) ? true : e.LastName.StartsWith(id))
                        orderby e.LastName
                        select new {
                            e.LastName, e.EmployeeID
                        }
                        ).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id) ///////////////////////
        {
            var employee = (from e in db.Employees
                                where e.EmployeeID == id
                                select e).FirstOrDefault();
            if (employee == null)
            {
                var ex = new Exception("registo nao encontrado");
                return View("error", ex);
            }

            var empregados = (from e in db.Employees select e).ToList();
            ViewBag.Empregados = empregados;
            return View("New",employee);
        }
        private System.Exception ErrorException(System.Exception ex)///////////////////////
        {
            if (ex.InnerException == null) { return ex; }
            else { return ErrorException(ex.InnerException); }
        }
        public ActionResult Index(Exception ex) ///////////////////////
        {
            var funcionarios = (from e in db.Employees orderby e.EmployeeID select e).ToList();
            return View(funcionarios);
        }
        public ActionResult New(Employee e) ///////////////////////
        {
            if (e.Adventista == null)
            {
                e.Adventista = false;
            }
            var funcionarios = (from c in db.Employees select c).ToList();
            ViewBag.Empregados = funcionarios;
            return View("New",new Employee());
        }

        public ActionResult NewJavaScript(int? id) ///////////////////////
        {
            Employee employee = new Employee();
            if (id != null)
            {
                employee = (from e in db.Employees
                                where e.EmployeeID == id
                                select e).FirstOrDefault();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Save(Employee e) 
        {
            try
            {
                if (e.EmployeeID != 0)
                {
                    db.Employees.Attach(e);
                    db.Entry(e).State = EntityState.Modified;
                }
                if (e.EmployeeID == 0)
                {
                    db.Employees.Add(e);
                }
                db.SaveChanges();
                var emps = (from emp in db.Employees select emp).ToList();
                ViewBag.Empregados = emps;
            }
            catch(Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }
            ViewBag.Message = "O Registro Foi Salvo com Sucesso";
            return View("New",new Employee());
        }


        [HttpPost]
        public ActionResult SaveJson(Employee e)
        {
            var resposta = new RespostaHtml { success = false };
            try
            {
                if (string.IsNullOrWhiteSpace(e.LastName))
                    resposta.message += "LastName Obrigatorio!";

                if (string.IsNullOrWhiteSpace(e.FirstName))
                    resposta.message += "FistName Obrigatorio!";

                if(resposta.message != null)
                    throw new Exception(resposta.message);
                e.Adventista = e.Adventista == null ? false : true;
                
                if (e.EmployeeID == 0)
                    db.Employees.Add(e);
                else
                    db.Employees.Attach(e);
                    db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                resposta.Data = e;
                resposta.success = true;
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
        public ActionResult DeleteJson(Employee e)
        {
            var resposta = new RespostaHtml { success = true };

            var employee = (from emp in db.Employees
                            where emp.EmployeeID == e.EmployeeID
                            select emp).FirstOrDefault();
            if (employee == null)
            {
                resposta.success = false;
                resposta.message = "Registro não Encontrado";
                return Json(resposta, JsonRequestBehavior.DenyGet);
            }
            try
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                resposta.Data = employee;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                resposta.success = false;
                resposta.message = ex.Message;
            }

            return Json(resposta, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int EmployeeID)
        {
            var employee = (from emp in db.Employees
                            where emp.EmployeeID == EmployeeID
                            select emp).FirstOrDefault();
            if (employee == null)
            {
                var ex = new Exception("O registro nao foi encontrado!");
                return View("Error", ex);
            }
            try
            {
                db.Employees.Remove(employee);
                db.SaveChanges();

                var emps = (from emp in db.Employees select emp).ToList();
                ViewBag.Empregados = emps;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }

            ViewBag.Message = "O registro foi Removido!";
            return View("New", new Employee());
        }

        public ActionResult IndexJavaScript(Exception ex) ///////////////////////
        {
            return View();
        }
        public ActionResult ListEmployeesJson(string FirstName) ///////////////////////
        {
            var funcionarios = (from e in db.Employees
                                where (string.IsNullOrEmpty(FirstName)
                                ? true : e.FirstName.StartsWith(FirstName))
                                orderby e.FirstName
                                select e).ToList();
            return Json( funcionarios,JsonRequestBehavior.AllowGet);
        }
    }
}