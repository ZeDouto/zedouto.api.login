using Microsoft.Extensions.DependencyInjection;
using Zedouto.Api.Login.Facade.Interfaces;
using Zedouto.Api.Login.Model.Configurations;
using Zedouto.Api.Login.Service.Extensions;

namespace Zedouto.Api.Login.Facade.Extensions
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