using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sneakers.Extensions;
using Sneakers.Infrastructure;
using Sneakers.Infrastructure.Repository;
using Sneakers.Models;
using Sneakers.Services.Implementation;
using Sneakers.Services.Interface;
using Sneakers.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneakers
{
    public class Startup
    {

        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();


            services.AddDbContext<AppDbContext>(options =>
            
                options.UseSqlServer(ConnectionString));

            //Configure Services

            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));
            services.ConfigureCors();
            services.ConfigureJWTService();
            services.ConfigureLoggerService();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IValidation, Validation>();
            services.AddTransient<IJwtHandler, JwtHandler>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ISizeService, SizeService>();
            services.AddTransient<ITypeService, TypeService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ISneakersService, SneakersService>();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sneakers", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sneakers v1"));
            }

            app.ConfigureCustomExceptionMiddleware();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            AppDbInitializer.Seed(app);
        }
    }
}
