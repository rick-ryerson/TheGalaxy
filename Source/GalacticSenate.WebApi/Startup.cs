using EventBus.RabbitMQ;
using GalacticSenate.Data.Extensions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Library.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

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

            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = "http://JediOrder.Web";
                   options.Audience = "api1";
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = false
                   };
               });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
             {
                   policy.RequireAuthenticatedUser();
                   policy.RequireClaim("scope", "api1");
               });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization("ApiScope");

                endpoints.MapGet("/", async context =>
             {
                   await context.Response.WriteAsync("Hello World from GalacticSenate.WebApi!");
               });
            });
        }
    }
}
