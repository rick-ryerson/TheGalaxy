using EventBus.RabbitMQ;
using GalacticSenate.Data.Extensions;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Services.Gender;
using GalacticSenate.Library.Services.MaritalStatusType;
using GalacticSenate.Library.Services.OrganizationNameValue;
using GalacticSenate.Library.Services.Party;
using GalacticSenate.Library.Services.PersonNameType;
using GalacticSenate.Library.Services.PersonNameValue;
using Microsoft.Extensions.DependencyInjection;
using System;

using Model = GalacticSenate.Domain.Model;

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

            services.AddSingleton<IEventsFactory<Model.Gender, int>, EventsFactory<Model.Gender, int>>();
            services.AddSingleton<IEventsFactory<Model.MaritalStatusType, int>, EventsFactory<Model.MaritalStatusType, int>>();
            services.AddSingleton<IEventsFactory<Model.OrganizationNameValue, int>, EventsFactory<Model.OrganizationNameValue, int>>();
            services.AddSingleton<IEventsFactory<Model.PersonNameType, int>, EventsFactory<Model.PersonNameType, int>>();
            services.AddSingleton<IEventsFactory<Model.PersonNameValue, int>, EventsFactory<Model.PersonNameValue, int>>();
            services.AddSingleton<IEventsFactory<Model.Party, Guid>, EventsFactory<Model.Party, Guid>>();

            return services;
        }
    }
}
