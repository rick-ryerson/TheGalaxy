using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4;
using Microsoft.IdentityModel.Tokens;

namespace JediOrder.Web {
   public class Startup {
      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services) {
         services.AddControllersWithViews();

         var builder = services.AddIdentityServer(options =>
         {
            options.IssuerUri = "http://JediOrder.Web;https://JediOrder.Web";
         })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);

         builder.AddDeveloperSigningCredential();

         services.AddAuthentication()
            .AddGoogle("Google", options =>
            {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

               options.ClientId = "asdasd";
               options.ClientSecret = "dsdfsdfsfd";
            })
            .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
            {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
               options.SignOutScheme = IdentityServerConstants.SignoutScheme;
               options.SaveTokens = true;

               options.Authority = "https://demo.identityserver.io/";
               options.ClientId = "interactive.confidential";
               options.ClientSecret = "secret";
               options.ResponseType = "code";

               options.TokenValidationParameters = new TokenValidationParameters
               {
                  NameClaimType = "name",
                  RoleClaimType = "role"
               };
            });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
         }

         app.UseStaticFiles();
         app.UseRouting();

         app.UseIdentityServer();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapGet("/", async context =>
               {
                  await context.Response.WriteAsync("Hello World from JediOrder.Web!");
               });
         });
      }
   }
}
