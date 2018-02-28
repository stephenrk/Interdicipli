using DataNorthwind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessNorthwind
{
    public class BusinessEmployee
    {
        private DataEmployee target = new DataEmployee();

        public Employee Save(Employee Employee)
        {
            if (string.IsNullOrWhiteSpace(Employee.LastName))
                throw new Exception("Last Name Invalido");
            if (string.IsNullOrWhiteSpace(Employee.FirstName))
                throw new Exception("First Name Invalido");
            return target.Save(Employee);
        }

        public void Delete(int ID)
        {
            target.Delete(ID);
        }

        public Employee GetById(int ID)
        {
            return target.GetById(ID);
        }
        public List<Employee> GetAllEmployees(Employee Employee)
        {
            return target.GetAllEmployees(Employee);
        }
    }
}
