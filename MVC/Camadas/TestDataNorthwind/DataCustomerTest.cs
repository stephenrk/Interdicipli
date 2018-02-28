using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataNorthwind;

namespace TestDataNorthwind
{
    [TestClass]
    public class DataCustomerTest
    {
        DataCustomer target = new DataCustomer();

        [TestMethod]
        public void TestSave()
        {
            var Customer = new Customer { CustomerID = "123", CompanyName = "Abobrinha" };
            target.Save(Customer);
            var registroDB = target.GetById(Customer.CustomerID);

            Assert.IsTrue(registroDB.CustomerID == Customer.CustomerID
                    && registroDB.CompanyName == Customer.CompanyName);

            if (registroDB.CustomerID == Customer.CustomerID
                && registroDB.CompanyName == Customer.CompanyName) { }
                

        }
    }
}
