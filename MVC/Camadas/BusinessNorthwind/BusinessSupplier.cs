using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataNorthwind;
using BusinessNorthwind;

namespace BusinessNorthwind
{
    public class BusinessSupplier
    {
        private DataSupplier target = new DataSupplier();

        public Supplier Save(Supplier Supplier)
        {
            // Validar o Nome da categoria
            if (string.IsNullOrWhiteSpace(Supplier.CompanyName))
                throw new Exception("Nome de Produto Invalido");

            // Caso tudo esteja ok
            return target.Save(Supplier);

        }

        public void Delete(int ID)
        {
            target.Delete(ID);
        }

        public Supplier GetById(int ID)
        {
            return target.GetById(ID);
        }
    }
}
