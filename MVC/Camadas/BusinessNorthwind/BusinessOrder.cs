using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataNorthwind;

namespace BusinessNorthwind
{
    public class BusinessOrder
    {
        private DataOrder target = new DataOrder();

        public Order Save(Order Order)
        {
            // Caso tudo esteja ok
            return target.Save(Order);

        }

        public void Delete(int ID)
        {
            target.Delete(ID);
        }

        public Order GetById(int ID)
        {
            return target.GetById(ID);
        }
    }
}
