using dataservice.Models;
using Microsoft.EntityFrameworkCore;
using NorthwindModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataservice.Code
{
    public class EmployeeProvider
    {

        private readonly _17SP2G4Context _context ;
        private readonly NorthwindEntities empContext;
        public List<User> Users { get; set; }

        public Dictionary<int, Employee> Employees { get; set; }

        public EmployeeProvider()
        {
            empContext = new NorthwindEntities(new Uri("http://services.odata.org/V3/Northwind/Northwind.svc"));
        }
        public EmployeeProvider(_17SP2G4Context context)
        {
            _context = context;
            empContext = new NorthwindEntities(new Uri("http://services.odata.org/V3/Northwind/Northwind.svc"));
        }

        public async Task refreshEmployees()
        {
            try
            {
                Employees = new Dictionary<int, Employee>();
                IEnumerable<Employee> tempEmps = await empContext.Employees.AddQueryOption("$select", "EmployeeID, LastName, FirstName, Title, TitleOfCourtesy, ReportsTo").ExecuteAsync();
                foreach (Employee emp in tempEmps.ToList())
                {
                    Employees[emp.EmployeeID] = emp;
                }
            }
            catch (Exception e)
            {
                e = null;
            }
                        
        }
    }
}
