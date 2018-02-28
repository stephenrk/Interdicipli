using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataNorthwind;
using BusinessNorthwind;

namespace NorthWindMVC.Controllers
{
    public class EmployeeController : Controller
    {
        BusinessEmployee BusinessEmployee = new BusinessEmployee();

        [HttpGet]
        public ActionResult ListEmployees()
        {
            var retorno = BusinessEmployee.GetAllEmployees(null);
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult NewJavaScript(int? Id)
        {
            Employee employee = new Employee();
            if (Id != null)
            {
                employee = BusinessEmployee.GetById(Id.Value);
            }
            return View(employee);
        }

        public ActionResult IndexJavaScript()
        {
            return View();
        }

        public ActionResult ListEmployeesJson(string FirstName)
        {
            var Employee = new Employee { FirstName = FirstName };
            var retorno = BusinessEmployee.GetAllEmployees(Employee);
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteJson(Employee e)
        {
            var resposta = new RespostaHtml { success = true };
            try
            {
                BusinessEmployee.Delete(e.EmployeeID);
                resposta.message = "Excluido com sucesso!";
            }
            catch (Exception ex)
            {
                resposta.success = false;
                ex = ErrorException(ex);
                resposta.message = ex.Message;
            }

            return Json(resposta, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int EmployeeId)
        {
            ViewBag.Message = "O Registro foi excluido com sucesso";

            try
            {
                BusinessEmployee.Delete(EmployeeId);
            }
            catch (Exception ex)
            {
                return View("Erro", ErrorException(ex));
            }

            ViewBag.Chefes = ObterChefes();
            return View("New", new Employee());
        }

        public ActionResult Edit(int id)
        {
            // Recupere o item do banco de dados
            var employee = BusinessEmployee.GetById(id);
            if (employee == null)
            {
                var ex = new Exception("Registro Não Encontrado");
                return View("Erro", ex);
            }

            ViewBag.Chefes = ObterChefes();
            // retorne a View "New", passando como parametro o registro 
            // retornado do banco de dados
            return View("New", employee);
        }

        public ActionResult Teste()
        {
            return View();
        }

        // GET: Employee
        public ActionResult Index()
        {
            BusinessEmployee BusinessEmployee = new BusinessEmployee();
            var retorno = BusinessEmployee.GetAllEmployees(null);
            return View(retorno);
        }
        public ActionResult New()
        {
            ViewBag.Chefes = ObterChefes();

            return View("New", new Employee());
        }

        public System.Exception ErrorException(System.Exception ex)
        {
            if (ex.InnerException == null)
                return ex;
            else
                return ErrorException(ex.InnerException);
        }


        // Esse método deve responder com JSON e não View
        [HttpPost]
        public ActionResult SaveJson(Employee e)
        {
            var resposta = new RespostaHtml { success = false };

            try
            {
                // Usar nossa classe de banco de dados
                e.Adventista = e.Adventista == null ? false : true;
                BusinessEmployee.Save(e);
                resposta.Data = e;
                resposta.success = true;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                resposta.message = ex.Message;
            }

            return Json(resposta, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Save(Employee e)
        {
            try
            {
                // Usar nossa classe de banco de dados
                e.Adventista = e.Adventista == null ? false : true;
                BusinessEmployee.Save(e);
                ViewBag.Chefes = ObterChefes();
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }

            // Se não ocorrer um erro, o que acontece?
            ViewBag.Message = "O Registro Foi Salvo com Sucesso!";
            return View("New", new Employee());
        }

        private List<Employee> ObterChefes()
        {
            return BusinessEmployee.GetAllEmployees(null);

        }
    }
}