using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataNorthwind;

namespace BusinessNorthwind
{
    public class BusinessCategory
    {
        private DataCategory target = new DataCategory();

        public List<Category> GetAllCategories()
        {
            return target.GetAllCategories();
        }
        public Category Save(Category Category)
        {
            if (string.IsNullOrWhiteSpace(Category.CategoryName))
                throw new Exception("nome da categoria invalido");
            return target.Save(Category);
        }

        public void Delete(int ID)
        {
            target.Delete(ID);
        }
        public Category GetByID(int ID)
        {
            return target.GetById(ID);
        }
    }
}
