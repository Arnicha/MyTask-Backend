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
using System.Threading.Tasks;
using mytask.Data;
using mytask.Handler;
using mytask.Repository;
using MyTask.BusinessFlow;
using MyTask.Service.Task;
using MyTask.Service.Todolist;
using MyTask.Service.Color;
using MyTask.BusinessLogic.CreateTaskBusinestLogic;
using MyTask.BusinessLogic.CreateTodolistBusinessLogic;

namespace mytask
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("postgres");
            var sqlConnectionStringReadOnly = Configuration.GetConnectionString("postgresReadOnly");
            services.AddScoped<MainContext>();

            //BusinessFlow
            services.AddScoped<HealthCheckBusinessFlow>();
            services.AddScoped<TaskBusinessFlow>();
            //BusinessLogic
            services.AddScoped<CreateTaskBusinessLogic>();
            services.AddScoped<CreateTodolistBusinessLogic>();
            //BusinessService
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITodolistService, TodolistService>();
            services.AddScoped<IColorService, ColorService>();
            //Repository
            services.AddScoped<IBaseRepository, BaseRepository>();

            services.AddDbContext<MainContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("postgres")
                ));
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:5000")
                                          .AllowAnyHeader()
                                          .AllowAnyOrigin()
                                          .AllowAnyMethod();
                                  });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();
            app.UseCors(option => option.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
