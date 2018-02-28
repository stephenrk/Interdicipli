using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessNorthwind;
using DataNorthwind;

namespace NorthwindServices
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Customer GetById(string ID);

        [OperationContract]
        Customer Save(Customer Customer);
    }
}
