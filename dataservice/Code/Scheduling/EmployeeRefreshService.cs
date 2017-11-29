using System;
using System.Threading;
using System.Threading.Tasks;

namespace dataservice.Code.Scheduling
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
