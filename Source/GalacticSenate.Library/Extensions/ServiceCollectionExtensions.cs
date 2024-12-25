using EventBus.RabbitMQ;
using GalacticSenate.Data.Extensions;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Services;
using GalacticSenate.Library.Services.PersonNameValue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Extensions {
    public static class ServiceCollectionExtensions {
        public class GenericLogger {

        }
        public static IServiceCollection AddPeopleAndOrganizations(this IServiceCollection services, EventBusSettings eventBusSettings, EfDataSettings efDataSettings) {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            if (eventBusSettings is null)
                throw new ArgumentNullException(nameof(eventBusSettings));
            if (efDataSettings is null)
                throw new ArgumentNullException(nameof(efDataSettings));

            services.AddTransient<ILogger>(s=>s.GetRequiredService<ILogger<GenericLogger>>());

            services.AddEntityFramework(efDataSettings);

            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IMaritalStatusTypeService, MaritalStatusTypeService>();
            services.AddScoped<IOrganizationNameValueService, OrganizationNameValueService>();
            services.AddScoped<IPersonNameTypeService, PersonNameTypeService>();
            services.AddScoped<IPersonNameValueService, PersonNameValueService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IInformalOrganizationService, InformalOrganizationService>();
            services.AddScoped<IFamilyService, FamilyService>();

            services.AddEventBus(eventBusSettings);

            services.AddSingleton<IEventsFactory, EventsFactory>();

            return services;
        }
    }
}
