using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Data.Extensions {
   public class EfDataSettings {
      public string ConnectionString { get; set; }
   }
   public static class ServiceCollectionExtensions {
      public static IServiceCollection AddEntityFramework(this IServiceCollection services, EfDataSettings settings) {
         if (settings is null) throw new ArgumentNullException(nameof(settings));

         services.AddLogging();

         services.AddDbContext<DataContext>(options =>
         {
            options.UseSqlServer(settings.ConnectionString);
         });

         services.AddScoped<IUnitOfWork<DataContext>, UnitOfWork>();

         services.AddScoped<IGenderRepository, GenderRepository>();
         services.AddScoped<IMaritalStatusTypeRepository, MaritalStatusTypeRepository>();
         services.AddScoped<IOrganizationRepository, OrganizationRepository>();
         services.AddScoped<IOrganizationNameRepository, OrganizationNameRepository>();
         services.AddScoped<IOrganizationNameValueRepository, OrganizationNameValueRepository>();
         services.AddScoped<IPartyRepository, PartyRepository>();
         services.AddScoped<IPersonNameTypeRepository, PersonNameTypeRepository>();
         services.AddScoped<IPersonNameValueRepository, PersonNameValueRepository>();

         return services;
      }
   }
}
