using NorthwindModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataservice.Connected_Services.Employee_Service
{
    public static class DataAccess
    {
        public static NorthwindEntities context = new NorthwindEntities(new Uri("http://services.odata.org/V3/Northwind/Northwind.svc"));
    }
}
