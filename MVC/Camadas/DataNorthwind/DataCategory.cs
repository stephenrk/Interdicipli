using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNorthwind
{
    public class DataCategory : Banco
    {
        public Category Save(Category Category)
        {
            if (Category.CategoryID == 0)
            {
                db.Categories.Add(Category);
            }
            else
            {
                db.Categories.Attach(Category);
                db.Entry(Category).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Category;
        }

        public Category GetById(int ID)
        {
            return (from c in db.Categories
                    where c.CategoryID == ID
                    select c).FirstOrDefault();
        }

        public void Delete (int ID)
        {
            var Category = GetById(ID);
            if (Category == null)
                throw new Exception("registro nao encontrado");
            db.Categories.Remove(Category);
            db.SaveChanges();
        }
        public List<Category> GetAllCategories()
        {
            return (from c in db.Categories
                    select c).ToList();
        }
    }
}
