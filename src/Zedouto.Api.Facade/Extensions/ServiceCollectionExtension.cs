using Microsoft.Extensions.DependencyInjection;
using Zedouto.Api.Facade.Interfaces;
using Zedouto.Api.Model.Configurations;
using Zedouto.Api.Service.Extensions;

namespace Zedouto.Api.Facade.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection services, FirestoreAppSettings firestoreSettings)
        {
            services.AddApplicationDependencies(firestoreSettings);
            services.AddSingleton<IUserFacade, UserFacade>();
            
            return services;
        }
    }
}