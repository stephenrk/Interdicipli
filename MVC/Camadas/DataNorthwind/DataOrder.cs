using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNorthwind
{
    public class DataOrder : Banco
    {
        public Order Save(Order Order)
        {
            if (Order.OrderID == 0)
            {
                db.Orders.Add(Order);
            }
            else
            {
                db.Orders.Attach(Order);
                db.Entry(Order).State =
                    System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Order;
        }
        public Order GetById(int ID)
        {
            return (from o in db.Orders
                    where o.OrderID == ID
                    select o).FirstOrDefault();
        }
        public void Delete(int ID)
        {
            var Order = GetById(ID);
            if (Order == null)
                throw new Exception("Registro Não Encontrado");

            db.Orders.Remove(Order);
            db.SaveChanges();
        }
    }
}
