using NorthWindMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindMVC.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> Products()
        {
            return (from p in db.Products select p).ToList();
        }
        private List<Category> Categories()
        {
            return (from c in db.Categories select c).ToList();
        }

        private NorthWind db = new NorthWind();
        public ActionResult Index()
        {
            var produtos = (from p in db.Products
                            .Include("Category")
                            .Include("Product")
                            select p).ToList();

            return View("Index",produtos);
        }
        private System.Exception ErrorException(System.Exception ex)
        {
            if (ex.InnerException == null) { return ex; }
            else { return ErrorException(ex.InnerException); }
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Categorias = Categories();
            ViewBag.Products = Products();

            var produtos = (from p in db.Products
                            where p.ProductID == id
                            select p).FirstOrDefault();
            if (produtos == null)
            {
                var ex = new Exception("registo nao encontrado");
                return View("error", ex);
            }
            return View("New", produtos);
        }
        public ActionResult New(Product p)
        {
            ViewBag.Categorias = Categories();
            ViewBag.Products = Products();

            if (p.Discontinued == null)
            {
                p.Discontinued = false;
            }

            var produto = (from c in db.Products select c).ToList();
            ViewBag.produto = produto;
            return View("New", new Product());
        }
        public ActionResult Save(Product p)
        {
            try
            {
                if (p.ProductID != 0)
                {
                    db.Products.Attach(p);
                    db.Entry(p).State = EntityState.Modified;
                }
                if (p.ProductID == 0)
                {
                    db.Products.Add(p);
                }
                db.SaveChanges();

                ViewBag.produto = Products();
                ViewBag.Categorias = Categories();
                ViewBag.Products = Products();

            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }
            ViewBag.Message = "O Registro Foi Salvo com Sucesso";
            return View("New", new Product());
        }

        public ActionResult Delete(int ProductID)
        {
            var produto = (from pro in db.Products
                            where pro.ProductID == ProductID
                            select pro).FirstOrDefault();
            if (produto == null)
            {
                var ex = new Exception("O registro nao foi encontrado!");
                return View("Error", ex);
            }
            try
            {
                db.Products.Remove(produto);
                db.SaveChanges();

                var pro = (from po in db.Products select po).ToList();
                ViewBag.Product = pro;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                return View("Erro", ex);
            }

            ViewBag.Message = "O registro foi Removido!";
            return View("New", new Product());
        }




        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public ActionResult ListProducts(int CategoryID)
        {
            var list = (from p in db.Products
                        where p.CategoryID == CategoryID
                        orderby p.ProductName
                        select new
                        {
                            p.ProductName,
                            p.ProductID
                        }
                        ).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult NewJavaScript(int? id) ///////////////////////
        {
            Product Product = new Product();
            if (id != null)
            {
                Product = (from p in db.Products
                            where p.ProductID == id
                            select p).FirstOrDefault();
            }
            return View(Product);
        }

        [HttpPost]
        public ActionResult SaveJson(Product e)
        {
            var resposta = new RespostaHtml { success = true };

            try
            {
                if (e.ProductID == 0)
                {
                    db.Products.Add(e);
                }
                else
                {
                    db.Products.Attach(e);
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
        public ActionResult DeleteJson(Product p)
        {
            var resposta = new RespostaHtml { success = true };

            var Product = (from emp in db.Products
                            where emp.ProductID == p.ProductID
                            select emp).FirstOrDefault();
            if (Product == null)
            {
                resposta.success = false;
                resposta.message = "Registro não Encontrado";
                return Json(resposta, JsonRequestBehavior.DenyGet);
            }
            try
            {
                db.Products.Remove(Product);
                db.SaveChanges();
                resposta.Data = Product;
            }
            catch (Exception ex)
            {
                ex = ErrorException(ex);
                resposta.success = false;
                resposta.message = ex.Message;
            }

            return Json(resposta, JsonRequestBehavior.DenyGet);
        }

       
        public ActionResult IndexJavaScript(Exception ex) ///////////////////////
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListProductsJson(int? CategoryID)
        {
            var list = (from p in db.Products
                        where p.CategoryID == CategoryID
                        orderby p.ProductName
                        select new
                        {
                            p.ProductName,
                            p.ProductID
                        }
                        ).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
    }
}