using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNorthwind
{
    public class DataEmployee : Banco
    {
        public Employee Save(Employee Employee)
        {
            if (Employee.EmployeeID == 0)
            {
                db.Employees.Add(Employee);
            }
            else
            {
                db.Employees.Attach(Employee);
                db.Entry(Employee).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Employee;
        }

        public Employee GetById(int ID)
        {
            return (from c in db.Employees
                    where c.EmployeeID == ID
                    select c).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var Employee = GetById(ID);
            if (Employee == null)
                throw new Exception("registro nao encontrado");
            db.Employees.Remove(Employee);
            db.SaveChanges();
        }
        public List<Employee> GetAllEmployees(Employee Employee)
        {
            bool SearchFirstName = false;
            string FirstName = "";

            if (Employee != null && string.IsNullOrWhiteSpace(Employee.FirstName))
            {
                SearchFirstName = true;
                FirstName = Employee.FirstName;
            }


            var emps = (from e in db.Employees
                        where (SearchFirstName ? e.FirstName.StartsWith(FirstName) : true)
                        orderby e.FirstName
                        select e).ToList();
            return emps;
        }
    }
}
