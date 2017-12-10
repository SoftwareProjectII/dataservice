using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using dataservice.Models;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using dataservice.Code.Scheduling;
using dataservice.Code;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;

// Swagger: https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "dataservice",
                        ValidAudience = "java app",
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };
                });

            var connection = @"Data Source = dt-srv-web4.ehb.local; Initial Catalog = 17SP2G4; Persist Security Info = True; User ID = 17SP2G4; Password = vj13dnpy25;";
            services.AddDbContext<_17SP2G4Context>(options => options.UseSqlServer(connection));

            services.AddSingleton<EmployeeProvider>(/*o =>new EmployeeProvider(new _17SP2G4Context(new DbContextOptionsBuilder<_17SP2G4Context>().UseSqlServer(connection).Options))*/);

            services.AddSingleton<IScheduledTask, EmailReminderTask>();
            services.AddSingleton<IScheduledTask, EmployeeRefreshTask>();

            services.AddScheduler((sender, args) =>
            {
                args.SetObserved();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Dataservice",
                    Version = "v1",
                    Contact = new Contact { Name = "Joshua", Email = "", Url = "https://www.youtube.com/watch?v=h6pPVYIpNFY" },
                    License = new License { Name = "Used under no license at all, I think", Url = "https://www.youtube.com/watch?v=xhH_PdKAigY&feature=youtu.be&t=2m25s" }
                });
            });

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
