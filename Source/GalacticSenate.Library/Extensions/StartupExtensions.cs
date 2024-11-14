using EventBus.RabbitMQ;
using GalacticSenate.Data.Extensions;
using GalacticSenate.Library.Services.Gender;
using GalacticSenate.Library.Services.Gender.Events;
using GalacticSenate.Library.Services.MaritalStatusType;
using GalacticSenate.Library.Services.MaritalStatusType.Events;
using GalacticSenate.Library.Services.OrganizationNameValue;
using GalacticSenate.Library.Services.OrganizationNameValue.Events;
using GalacticSenate.Library.Services.Party;
using GalacticSenate.Library.Services.Party.Events;
using GalacticSenate.Library.Services.PersonNameType;
using GalacticSenate.Library.Services.PersonNameType.Events;
using GalacticSenate.Library.Services.PersonNameValue;
using GalacticSenate.Library.Services.PersonNameValue.Events;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GalacticSenate.Library.Extensions {
    public static class StartupExtensions {
        public static IServiceCollection AddPeopleAndOrganizations(this IServiceCollection services, EventBusSettings eventBusSettings, EfDataSettings efDataSettings) {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            if (eventBusSettings is null)
                throw new ArgumentNullException(nameof(eventBusSettings));
            if (efDataSettings is null)
                throw new ArgumentNullException(nameof(efDataSettings));


            services.AddEntityFramework(efDataSettings);

            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IMaritalStatusTypeService, MaritalStatusTypeService>();
            services.AddScoped<IOrganizationNameValueService, OrganizationNameValueService>();
            services.AddScoped<IPersonNameTypeService, PersonNameTypeService>();
            services.AddScoped<IPersonNameValueService, PersonNameValueService>();
            services.AddScoped<IPartyService, PartyService>();

            services.AddEventBus(eventBusSettings);

            services.AddSingleton<IGenderEventsFactory, GenderEventsFactory>();
            services.AddSingleton<IMaritalStatusTypeEventsFactory, MaritalStatusTypeEventsFactory>();
            services.AddSingleton<IOrganizationNameValueEventsFactory, OrganizationNameValueEventsFactory>();
            services.AddSingleton<IPersonNameTypeEventsFactory, PersonNameTypeEventsFactory>();
            services.AddSingleton<IPersonNameValueEventsFactory, PersonNameValueEventsFactory>();
            services.AddSingleton<IPartyEventsFactory, PartyEventsFactory>();

            return services;
        }
    }
}
