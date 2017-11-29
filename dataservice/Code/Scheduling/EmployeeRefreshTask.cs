using dataservice.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NorthwindModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace dataservice.Code.Scheduling
{
    public class EmployeeRefreshTask : IScheduledTask
    {
        private _17SP2G4Context context;
        private NorthwindEntities empContext;
        private EmployeeProvider employeeProvider;

        public string Schedule => "*/1 * * * *";

        public EmployeeRefreshTask(EmployeeProvider employeeProvider)
        {
            this.employeeProvider = employeeProvider;
        }

        public async Task Invoke(_17SP2G4Context context, CancellationToken cancellationToken)
        {
            this.context = context;
            empContext = new NorthwindEntities(new Uri("http://services.odata.org/V3/Northwind/Northwind.svc"));
            await refreshEmployees();
            await loadEmpsIntoUsers();
        }

        public async Task loadEmpsIntoUsers()
        {
            List<int?> emps = context.User.Where(m => m.EmpId != null).Select(m => m.EmpId).ToList();
            Dictionary<int, Employee> Employees =  employeeProvider.Employees.Where(m => !emps.Contains(m.Value.EmployeeID))
                .ToDictionary(m => m.Key, m => m.Value);

            foreach (Employee e in Employees.Values)
            {
                var salt = getSalt();
                User user = new User();
                user.Username = e.FirstName[0] + e.LastName;
                user.EmpId = e.EmployeeID;
                user.Salt = Convert.ToBase64String(salt);
                user.Password = HashPassword(e.EmployeeID.ToString() + e.LastName, salt);

                context.User.Add(user);
            }
            await context.SaveChangesAsync();            
        }
        private string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
        private byte[] getSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        private async Task refreshEmployees()
        {
            try
            {
                Dictionary<int, Employee> Employees = new Dictionary<int, Employee>();
                IEnumerable<Employee> tempEmps = await empContext.Employees.AddQueryOption("$select", "EmployeeID, LastName, FirstName, Title, TitleOfCourtesy, ReportsTo").ExecuteAsync();
                foreach (Employee emp in tempEmps.ToList())
                {
                    Employees[emp.EmployeeID] = emp;
                }

                employeeProvider.Employees = Employees;
            }
            catch (Exception e)
            {
                e = null;
            }
        }
    }
}
