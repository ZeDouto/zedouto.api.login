using Microsoft.Extensions.DependencyInjection;
using Zedouto.Api.Facade.Interfaces;
using Zedouto.Api.Model.Configurations;

namespace Zedouto.Api.Facade.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection services)
        {
            services.AddSingleton<IUserFacade, UserFacade>();
            
            return services;
        }
    }
}