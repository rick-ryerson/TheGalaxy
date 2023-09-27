using EventBus.RabbitMQ;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Library.Gender;
using GalacticSenate.Library.MaritalStatusType;
using GalacticSenate.Library.OrganizationNameValue;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Extensions {
   public static class StartupExtensions {
      public static IServiceCollection AddPeopleAndOrganizations(this IServiceCollection services, EventBusSettings settings) {
         if (settings is null)
            throw new ArgumentNullException(nameof(settings));


         services.AddScoped<IUnitOfWork<DataContext>, UnitOfWork>();

         services.AddScoped<IGenderService, GenderService>();
         services.AddScoped<IMaritalStatusTypeService, MaritalStatusTypeService>();
         services.AddScoped<IOrganizationNameValueService, OrganizationNameValueService>();

         services.AddEventBus(settings);
         return services;
      }
   }
}
