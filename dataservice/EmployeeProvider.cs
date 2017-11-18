using dataservice.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using NorthwindModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace dataservice
{
    public class EmployeeProvider
    {
        private readonly NorthwindEntities empContext;
        public List<User> Users { get; set; }

        public Dictionary<int, Employee> Employees { get; set; }

        public EmployeeProvider()
        {
            empContext = new NorthwindEntities(new Uri("http://services.odata.org/V3/Northwind/Northwind.svc"));
        }

        public async Task refreshEmployees()
        {
            Employees = new Dictionary<int, Employee>();
            IEnumerable<Employee> tempEmps = await empContext.Employees.AddQueryOption("$select", "EmployeeID, LastName, FirstName, Title, TitleOfCourtesy, ReportsTo").ExecuteAsync();
            foreach (Employee emp in tempEmps)
            {
                Employees[emp.EmployeeID] = emp;
            }                  
        }
    }
}
