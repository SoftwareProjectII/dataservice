using dataservice.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using NorthwindModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace dataservice
{
    public class EmployeeRefreshService : HostedService
    {
        private readonly EmployeeProvider employeeProvider;

        public EmployeeRefreshService(EmployeeProvider employeeProvider)
        {
            this.employeeProvider = employeeProvider;

        }        

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await employeeProvider.refreshEmployees();
                await Task.Delay(TimeSpan.FromMinutes(15), cancellationToken);
            }
        }
    }
}
