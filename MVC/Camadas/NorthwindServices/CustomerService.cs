using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataNorthwind;
using BusinessNorthwind;

namespace NorthwindServices
{
    public class CustomerService : ICustomerService
    {
        public Customer GetById(string ID)
        {
            var Customer = (new BusinessCustomer()).GetById(ID);
            return Customer;
        }
        public Customer Save(Customer Customer)
        {
            var customer = (new BusinessCustomer()).Save(Customer);
            return customer;
        }
    }
}
