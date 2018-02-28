using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNorthwind
{
    public class DataCustomer : Banco
    {
        public List<Customer> GetAllCustomers(Customer Customer)
        {
            var customers = (from c in db.Customers
                             where
                             (string.IsNullOrEmpty(Customer.CompanyName) ? true : c.CompanyName.Contains(Customer.CompanyName))
                             && (string.IsNullOrEmpty(Customer.Country) ? true : c.Country.Contains(Customer.Country))
                             orderby c.CompanyName
                             select c).ToList();
            return customers;

        }
        public object ListCountries(string id)
        {
            var list = (from c in db.Customers
                        where c.Country != null
                        && (string.IsNullOrEmpty(id) ? true : c.Country.StartsWith(id))
                        orderby c.Country
                        select c.Country
                          ).ToList().Distinct();
            return list;
        }
        public Customer Save(Customer Customer)
        {
            var customerDB = GetById(Customer.CustomerID);
            if (customerDB == null)
            {
                db.Customers.Add(Customer);
            }
            else
            {
                db.Entry(customerDB).State = System.Data.Entity.EntityState.Detached;
                db.Customers.Attach(Customer);
                db.Entry(Customer).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Customer;
        }

        public Customer GetById(string ID)
        {
            return (from c in db.Customers
                    where c.CustomerID == ID
                    select c).FirstOrDefault();
        }

        public void Delete(string ID)
        {
            var Customer = GetById(ID);
            if (Customer == null)
                throw new Exception("Registro não encontrado");
            db.Customers.Remove(Customer);
            db.SaveChanges();
        }
    }
}
