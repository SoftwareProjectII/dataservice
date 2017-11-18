using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using dataservice.Models;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dataservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connection = @"Data Source = dt-srv-web4.ehb.local; Initial Catalog = 17SP2G4; Persist Security Info = True; User ID = 17SP2G4; Password = vj13dnpy25;";
            services.AddDbContext<_17SP2G4Context>(options => options.UseSqlServer(connection));

            services.AddSingleton<EmployeeProvider>();
            services.AddSingleton<IHostedService, EmployeeRefreshService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
