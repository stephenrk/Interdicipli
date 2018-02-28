using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataNorthwind;

namespace BusinessNorthwind
{
    public class BusinessCustomer
    {
        private DataCustomer target = new DataCustomer();
        public List<Customer> GetAllCustomers(Customer Customer)
        {
            return target.GetAllCustomers(Customer);
        }
        public object ListCountries(string id)
        {
            return target.ListCountries(id);
        }
        public Customer Save(Customer Customer)
        {
            if (string.IsNullOrWhiteSpace(Customer.CustomerID))
                throw new Exception("Codigo de cliente invalido");
            if (string.IsNullOrWhiteSpace(Customer.CompanyName))
                throw new Exception("Nome de Categoria Invalido");
            return target.Save(Customer);
        }
        public void Delete(string ID)
        {
            target.Delete(ID);
        }
        public Customer GetById(string ID)
        {
            return target.GetById(ID);
        }
    }
}
