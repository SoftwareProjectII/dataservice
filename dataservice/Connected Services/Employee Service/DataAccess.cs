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
        private static Dictionary<int, Employee> Employees = null;

        public async static Task<List<Employee>> GetEmployees()
        {
            await refreshIfEmployeesEmpty();
            return Employees.Values.ToList();
        }

        public async static Task<Employee> GetEmployeeByID(int id)
        {
            await refreshIfEmployeesEmpty();
            return Employees.Where(x => x.Key == id).FirstOrDefault().Value;
        }

        public async static Task refreshEmployees()
        {
            Employees = new Dictionary<int, Employee>();
            IEnumerable<Employee> test = await context.Employees.AddQueryOption("$select", "EmployeeID, LastName, FirstName, Title, TitleOfCourtesy, ReportsTo").ExecuteAsync();
            foreach (Employee emp in test)
            {                
                Employees[emp.EmployeeID] = new Employee {EmployeeID = emp.EmployeeID, LastName = emp.LastName, FirstName = emp.FirstName,
                    Title = emp.Title, TitleOfCourtesy = emp.TitleOfCourtesy, ReportsTo = emp.ReportsTo };
            }
        }

        private async static Task refreshIfEmployeesEmpty ()
        {
            if (Employees == null)
            {
                await refreshEmployees();
            }
        }
    }
}
