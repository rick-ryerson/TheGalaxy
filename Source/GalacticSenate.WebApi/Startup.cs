using EventBus.RabbitMQ;
using GalacticSenate.Data.Extensions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library.Extensions;
using GalacticSenate.Library.Gender;
using GalacticSenate.Library.MaritalStatusType;
using GalacticSenate.Library.OrganizationName;
using GalacticSenate.Library.OrganizationNameValue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GalacticSenate.WebApi {
   public class Startup {
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services) {
         var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
         var connectionString = Configuration.GetConnectionString("DataContext");

         services.AddDbContext<DataContext>(options =>
         {
            options.UseSqlServer(connectionString);
         });

         EventBusSettings eventBusSettings = new EventBusSettings();
         Configuration.Bind(EventBusSettings.SectionName, eventBusSettings);
         EfDataSettings efDataSettings = new EfDataSettings()
         {
            ConnectionString = connectionString
         };

         services.AddPeopleAndOrganizations(eventBusSettings, efDataSettings);

         services.AddControllers()
             .AddJsonOptions(options =>
             {
                options.JsonSerializerOptions
                       .Converters
                       .Add(new JsonStringEnumConverter());
             });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();

            endpoints.MapGet("/", async context =>
            {
               await context.Response.WriteAsync("Hello World from GalacticSenate.WebApi!");
            });
         });
      }
   }
}
