using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataNorthwind;

namespace BusinessNorthwind
{
    public class BusinessProduct
    {
        private DataProduct target = new DataProduct();

        public Product Save(Product Product)
        {
            // Validar o Nome da categoria
            if (string.IsNullOrWhiteSpace(Product.ProductName))
                throw new Exception("Nome de Produto Invalido");

            // Caso tudo esteja ok
            return target.Save(Product);

        }

        public void Delete(int ID)
        {
            target.Delete(ID);
        }

        public Product GetById(int ID)
        {
            return target.GetById(ID);
        }
    }
}
