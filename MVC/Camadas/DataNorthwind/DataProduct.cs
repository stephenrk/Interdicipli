using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNorthwind
{
    public class DataProduct : Banco
    {
        public Product Save(Product Product)
        {
            if (Product.ProductID == 0)
            {
                db.Products.Add(Product);
            }
            else
            {
                db.Products.Attach(Product);
                db.Entry(Product).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Product;
        }

        public Product GetById(int ID)
        {
            return (from c in db.Products
                    where c.ProductID == ID
                    select c).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var Product = GetById(ID);
            if (Product == null)
                throw new Exception("registro nao encontrado");
            db.Products.Remove(Product);
            db.SaveChanges();
        }
    }
}
