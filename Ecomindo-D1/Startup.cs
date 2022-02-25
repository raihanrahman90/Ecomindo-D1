using Ecomindo_D1.Model;
using Ecomindo_D1.Interface;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ecomindo_D1.Services;
using Ecomindo_D1.Job;

namespace Ecomindo_D1
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

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers();

            services.AddDbContext<OnBoardingSkdDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IMenuService, MenuService>();

            services.AddHostedService<MenuMessageListener>();

            services.AddTransient<LogTimeJob>();
            services.AddTransient<QuartzJobFactory>();


            services.AddSingleton<ISchedulerService, SchedulerService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Tutorial Net Core", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Tutorial Net Core v1");
            });

            var schedulerService = app.ApplicationServices.GetRequiredService<ISchedulerService>();
            schedulerService.Initialize();
            schedulerService.Start();
        }
    }
}