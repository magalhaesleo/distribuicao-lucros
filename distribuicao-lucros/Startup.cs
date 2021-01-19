using AutoMapper;

using distribuicao_lucros_application.Features.Employees;
using distribuicao_lucros_application.Features.Employees.Mappers;

using distribuicao_lucros_domain.Features.Employees;

using distribuicao_lucros_infra_data.Features.Employees;

using Firebase.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using System.Threading.Tasks;

namespace distribuicao_lucros
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "distribuicao_lucros", Version = "v1" });
            });
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            string databaseUrl = Configuration["DatabaseUrl"];
            string databaseSecret = Configuration["DatabaseSecret"];

            services.AddTransient(fb => new FirebaseClient(databaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret) }));

            services.AddAutoMapper(typeof(EmployeeProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "distribuicao_lucros v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
