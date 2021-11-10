using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PmfBackend.Services;
using Npgsql;
using PmfBackend.Database;

namespace PmfBackend
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PmfBackend", Version = "v1" });
            });
          ///  services.AddDbContext<TransactionDbContext>(options =>
           // {
             //   options.UseNpgsql(CreateConnectionString());
           // });
            services.AddScoped<ITransactionService, TransactionSerive>();

           //services.AddDBContext
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PmfBackend v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
      //      InitializeDatabase(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
         private string CreateConnectionString()
        {
            var username = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? this.Configuration["Database:Username"];
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? this.Configuration["Database:Password"];
            var host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? this.Configuration["Database:Host"];
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? this.Configuration["Database:Port"];
            var database = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? this.Configuration["Database:Name"];

            var builder = new NpgsqlConnectionStringBuilder()
            {
                Username = username,
                Password = password,
                Host = host,
                Port = int.Parse(port),
                Database = database,
                Pooling = true
            };

            return builder.ConnectionString;
        }
   //      private void InitializeDatabase(IApplicationBuilder app, IWebHostEnvironment env)
   //     {
     //       if (env.IsDevelopment())
       //     {
         //       using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
           //     {
               // }
         //   }
     //   }
    }
}
